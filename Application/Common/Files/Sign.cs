using Application.Interfaces;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Persistence;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Files
{
  public class Sign
  {
    public class Command : IRequest<MemoryStream>
    {
      public string FileName { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(x => x.FileName).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, MemoryStream>
    {
      private const string BaseDir = "Assets";
      private readonly IDbContext _context;
      private readonly IUserAccessor _userAccessor;
      private readonly IWebHostEnvironment _env;
      private readonly IEncryptionHelper _encryption;

      public Handler(IDbContext context, IUserAccessor userAccessor, IWebHostEnvironment env,
        IEncryptionHelper encryption)
      {
        _context = context;
        _userAccessor = userAccessor;
        _env = env;
        _encryption = encryption;
      }


      public async Task<MemoryStream> Handle(Command request, CancellationToken cancellationToken)
      {
        //Get User Profile
        var webuser = await _context.WebUser.FindByIdAsync(_userAccessor.GetCurrentUsername());

        if (webuser == null) throw new ApiException((int)HttpStatusCode.NotFound);

        var digitalIdPath = Path.Combine(_env.ContentRootPath, BaseDir, webuser.DigitalIdFile);

        var docPath = Path.Combine(_env.ContentRootPath, BaseDir, request.FileName);

        if (!File.Exists(docPath))
          throw new ApiException("Not Found", (int)HttpStatusCode.NotFound);


        FileStream docStream = new FileStream(docPath, FileMode.Open, FileAccess.ReadWrite);

        PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);

        // Add Page
        PdfPageBase newPage = loadedDocument.Pages.Add();

        PdfGraphics graphics = newPage.Graphics;


        //Gets the first page of the document
        PdfLoadedPage page = loadedDocument.Pages[0] as PdfLoadedPage;

        //Creates a certificate
        FileStream certificateStream = new FileStream(digitalIdPath, FileMode.Open, FileAccess.Read);

        PdfCertificate certificate = new PdfCertificate(certificateStream, _encryption.Decrypt(webuser.DigitalIdPwd));

        PdfSignature signature = new PdfSignature(loadedDocument, page, certificate, "Signature");

        //Sets an image for signature field
        var signImage = webuser.SignImg.Substring(webuser.SignImg.IndexOf(",", StringComparison.Ordinal) + 1);

        await using var imageStream = new MemoryStream(Convert.FromBase64String(signImage));

        //Sets an image for signature field
        PdfBitmap signatureImage = new PdfBitmap(imageStream);

        //Sets signature information
        signature.Bounds = new RectangleF(new PointF(0, 0), signatureImage.PhysicalDimension);

        //Draws the signature image
        graphics.DrawImage(signatureImage, 0, 0);

        //Save the document into stream
        MemoryStream stream = new MemoryStream();

        loadedDocument.Save(stream);

        stream.Position = 0;

        //Close the documents
        loadedDocument.Close(true);

        return stream;
      }
    }
  }
}