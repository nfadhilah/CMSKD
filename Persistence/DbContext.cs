using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Auth;
using Persistence.Repository.BUD;
using Persistence.Repository.DM;
using Persistence.Repository.MA;
using Persistence.Repository.TUBEND;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
  public interface IDbContext : IDisposable
  {
    IDbConnection Connection { get; }
    BendRepository Bend { get; }
    DaftPhk3Repository DaftPhk3 { get; }
    JBankRepository JBank { get; }
    JBendRepository JBend { get; }
    JDanaRepository JDana { get; }
    JBKasRepository JBKas { get; }
    JBMRepository JBM { get; }
    JTransRepository JTrans { get; }
    JAKasRepository JAKas { get; }
    JnsAkunRepository JnsAkun { get; }
    JSatuanRepository JSatuan { get; }
    JBayarRepository JBayar { get; }
    JUsahaRepository JUsaha { get; }
    ProfilRepository Profil { get; }
    DaftFungsiRepository DaftFungsi { get; }
    DaftBankRepository DaftBank { get; }
    BkBKasRepository BkBKas { get; }
    PegawaiRepository Pegawai { get; }
    ZKodeRepository ZKode { get; }
    PPKRepository PPK { get; }
    DPARepository DPA { get; }
    DPABRepository DPAB { get; }
    DPADetBRepository DPADetB { get; }
    DPADanaBRepository DPADanaB { get; }
    DPABlnBRepository DPABlnB { get; }
    DPARRepository DPAR { get; }
    DPADetRRepository DPADetR { get; }
    DPADanaRRepository DPADanaR { get; }
    DPABlnRRepository DPABlnR { get; }
    PgrmUnitRepository PgrmUnit { get; }
    DaftRekeningRepository DaftRekening { get; }
    PajakRepository Pajak { get; }
    DaftUnitRepository DaftUnit { get; }
    JTahunRepository JTahun { get; }
    MPgrmRepository MPgrm { get; }
    MKegiatanRepository MKegiatan { get; }
    KegUnitRepository KegUnit { get; }
    UrusanUnitRepository UrusanUnit { get; }
    GolonganRepository Golongan { get; }
    SifatKegRepository SifatKeg { get; }
    StruRekRepository StruRek { get; }
    StruUnitRepository StruUnit { get; }
    KPARepository KPA { get; }
    BendKPARepository BendKPA { get; }
    NrcBendRepository NrcBend { get; }
    JabTtdRepository JabTtd { get; }
    StatTrsRepository StatTrs { get; }
    WebUserRepository WebUser { get; }
    WebAppRepository WebAbb { get; }
    WebRoleRepository WebRole { get; }
    WebGroupRepository WebGroup { get; }
    WebOtorRepository WebOtor { get; }
    KontrakRepository Kontrak { get; }
    BeritaRepository Berita { get; }
    BeritaDetRRepository BeritaDetR { get; }
    SPDRepository SPD { get; }
    SPDDetBRepository SPDDetB { get; }
    SPDDetRRepository SPDDetR { get; }
    SPPRepository SPP { get; }
    SPPDetBRepository SPPDetB { get; }
    SPPDetRRepository SPPDetR { get; }
    SPPDetBDanaRepository SPPDetBDana { get; }
    SPPDetRDanaRepository SPPDetRDana { get; }
    SPPDetRPRepository SPPDetRP { get; }
    JTrnlKasRepository JTrnlKas { get; }
    BkBankRepository BkBank { get; }
    BkBankDetRepository BkBankDet { get; }
    TBPLRepository TBPL { get; }
    TBPLDetRepository TBPLDet { get; }
    TBPLDetKegRepository TBPLDetKeg { get; }
    BPKRepository BPK { get; }
    BPKDetRRepository BPKDetR { get; }
    BPKDetRDanaRepository BPKDetRDana { get; }
    BPKPajakStrRepository BPKPajakStr { get; }
    BPKDetRPRepository BPKDetRP { get; }
    BkPajakRepository BkPajak { get; }
    BkPajakDetStrRepository BkPajakDetStr { get; }
    STSRepository STS { get; }
    STSDetRRepository STSDetR { get; }
    STSDetDRepository STSDetD { get; }
    STSDetBRepository STSDetB { get; }
    SPMRepository SPM { get; }
    SP2DRepository SP2D { get; }
    SP2DDetRRepository SP2DDetR { get; }
    SP2DDetRDanaRepository SP2DDetRDana { get; }
    SP2DDetRPRepository SP2DDetRP { get; }
    SP2DDetBRepository SP2DDetB { get; }
    SP2DDetBDanaRepository SP2DDetBDana { get; }
    BKUDRepository BKUD { get; }
    BKUKRepository BKUK { get; }
    DPRepository DP { get; }
    DPDetRepository DPDet { get; }
    WebSetRepository WebSet { get; }
    SPPBARepository SPPBA { get; }
    SPPBPKRepository SPPBPK { get; }
    TahapRepository Tahap { get; }
  }

  public class DbContext : IDbContext
  {
    private bool _disposed;

    public DbContext(string connectionString)
    {
      MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public IDbConnection Connection { get; }
    public DaftUnitRepository DaftUnit => new DaftUnitRepository(Connection);
    public BendRepository Bend => new BendRepository(Connection);
    public DaftPhk3Repository DaftPhk3 => new DaftPhk3Repository(Connection);
    public JBankRepository JBank => new JBankRepository(Connection);
    public JBendRepository JBend => new JBendRepository(Connection);
    public JDanaRepository JDana => new JDanaRepository(Connection);
    public JBKasRepository JBKas => new JBKasRepository(Connection);
    public JBMRepository JBM => new JBMRepository(Connection);
    public JTransRepository JTrans => new JTransRepository(Connection);
    public JAKasRepository JAKas => new JAKasRepository(Connection);
    public JnsAkunRepository JnsAkun => new JnsAkunRepository(Connection);
    public JSatuanRepository JSatuan => new JSatuanRepository(Connection);
    public JBayarRepository JBayar => new JBayarRepository(Connection);
    public JUsahaRepository JUsaha => new JUsahaRepository(Connection);
    public ProfilRepository Profil => new ProfilRepository(Connection);
    public DaftBankRepository DaftBank => new DaftBankRepository(Connection);
    public DaftFungsiRepository DaftFungsi => new DaftFungsiRepository(Connection);
    public BkBKasRepository BkBKas => new BkBKasRepository(Connection);
    public PegawaiRepository Pegawai => new PegawaiRepository(Connection);
    public ZKodeRepository ZKode => new ZKodeRepository(Connection);
    public PPKRepository PPK => new PPKRepository(Connection);
    public DPARepository DPA => new DPARepository(Connection);
    public DPABRepository DPAB => new DPABRepository(Connection);
    public DPADetBRepository DPADetB => new DPADetBRepository(Connection);
    public DPADanaBRepository DPADanaB => new DPADanaBRepository(Connection);
    public DPABlnBRepository DPABlnB => new DPABlnBRepository(Connection);
    public DPARRepository DPAR => new DPARRepository(Connection);
    public DPADetRRepository DPADetR => new DPADetRRepository(Connection);
    public DPADanaRRepository DPADanaR => new DPADanaRRepository(Connection);
    public DPABlnRRepository DPABlnR => new DPABlnRRepository(Connection);
    public PgrmUnitRepository PgrmUnit => new PgrmUnitRepository(Connection);
    public DaftRekeningRepository DaftRekening => new DaftRekeningRepository(Connection);
    public PajakRepository Pajak => new PajakRepository(Connection);
    public JTahunRepository JTahun => new JTahunRepository(Connection);
    public MPgrmRepository MPgrm => new MPgrmRepository(Connection);
    public MKegiatanRepository MKegiatan => new MKegiatanRepository(Connection);
    public KegUnitRepository KegUnit => new KegUnitRepository(Connection);
    public UrusanUnitRepository UrusanUnit => new UrusanUnitRepository(Connection);
    public GolonganRepository Golongan => new GolonganRepository(Connection);
    public SifatKegRepository SifatKeg => new SifatKegRepository(Connection);
    public StruRekRepository StruRek => new StruRekRepository(Connection);
    public StruUnitRepository StruUnit => new StruUnitRepository(Connection);
    public KPARepository KPA => new KPARepository(Connection);
    public BendKPARepository BendKPA => new BendKPARepository(Connection);
    public NrcBendRepository NrcBend => new NrcBendRepository(Connection);
    public StatTrsRepository StatTrs => new StatTrsRepository(Connection);
    public JabTtdRepository JabTtd => new JabTtdRepository(Connection);
    public WebUserRepository WebUser => new WebUserRepository(Connection);
    public WebAppRepository WebAbb => new WebAppRepository(Connection);
    public WebRoleRepository WebRole => new WebRoleRepository(Connection);
    public WebGroupRepository WebGroup => new WebGroupRepository(Connection);
    public WebOtorRepository WebOtor => new WebOtorRepository(Connection);
    public KontrakRepository Kontrak => new KontrakRepository(Connection);
    public BeritaRepository Berita => new BeritaRepository(Connection);
    public BeritaDetRRepository BeritaDetR => new BeritaDetRRepository(Connection);
    public SPDRepository SPD => new SPDRepository(Connection);
    public SPDDetBRepository SPDDetB => new SPDDetBRepository(Connection);
    public SPDDetRRepository SPDDetR => new SPDDetRRepository(Connection);
    public SPPRepository SPP => new SPPRepository(Connection);
    public SPPDetBRepository SPPDetB => new SPPDetBRepository(Connection);
    public SPPDetRRepository SPPDetR => new SPPDetRRepository(Connection);
    public SPPDetBDanaRepository SPPDetBDana => new SPPDetBDanaRepository(Connection);
    public SPPDetRDanaRepository SPPDetRDana => new SPPDetRDanaRepository(Connection);
    public SPPDetRPRepository SPPDetRP => new SPPDetRPRepository(Connection);
    public JTrnlKasRepository JTrnlKas => new JTrnlKasRepository(Connection);
    public BkBankRepository BkBank => new BkBankRepository(Connection);
    public BkBankDetRepository BkBankDet => new BkBankDetRepository(Connection);
    public TBPLRepository TBPL => new TBPLRepository(Connection);
    public TBPLDetRepository TBPLDet => new TBPLDetRepository(Connection);
    public TBPLDetKegRepository TBPLDetKeg => new TBPLDetKegRepository(Connection);
    public BPKRepository BPK => new BPKRepository(Connection);
    public BPKDetRRepository BPKDetR => new BPKDetRRepository(Connection);
    public BPKDetRDanaRepository BPKDetRDana => new BPKDetRDanaRepository(Connection);
    public BPKPajakStrRepository BPKPajakStr => new BPKPajakStrRepository(Connection);
    public BPKDetRPRepository BPKDetRP => new BPKDetRPRepository(Connection);
    public BkPajakRepository BkPajak => new BkPajakRepository(Connection);
    public BkPajakDetStrRepository BkPajakDetStr => new BkPajakDetStrRepository(Connection);
    public STSRepository STS => new STSRepository(Connection);
    public STSDetRRepository STSDetR => new STSDetRRepository(Connection);
    public STSDetDRepository STSDetD => new STSDetDRepository(Connection);
    public STSDetBRepository STSDetB => new STSDetBRepository(Connection);
    public SPMRepository SPM => new SPMRepository(Connection);
    public SP2DRepository SP2D => new SP2DRepository(Connection);
    public SP2DDetRRepository SP2DDetR => new SP2DDetRRepository(Connection);
    public SP2DDetRDanaRepository SP2DDetRDana => new SP2DDetRDanaRepository(Connection);
    public SP2DDetRPRepository SP2DDetRP => new SP2DDetRPRepository(Connection);
    public SP2DDetBRepository SP2DDetB => new SP2DDetBRepository(Connection);
    public SP2DDetBDanaRepository SP2DDetBDana => new SP2DDetBDanaRepository(Connection);
    public BKUDRepository BKUD => new BKUDRepository(Connection);
    public BKUKRepository BKUK => new BKUKRepository(Connection);
    public DPRepository DP => new DPRepository(Connection);
    public DPDetRepository DPDet => new DPDetRepository(Connection);
    public WebSetRepository WebSet => new WebSetRepository(Connection);
    public SPPBARepository SPPBA => new SPPBARepository(Connection);
    public SPPBPKRepository SPPBPK => new SPPBPKRepository(Connection);
    public TahapRepository Tahap => new TahapRepository(Connection);

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          Connection?.Dispose();
        }
      }
      _disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~DbContext()
    {
      Dispose(false);
    }
  }
}