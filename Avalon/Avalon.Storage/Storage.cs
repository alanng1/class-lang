namespace Avalon.Storage;

public class Storage : Any
{
    public override bool Init()
    {
        base.Init();
        this.InternIntern = InternIntern.This;
        this.InternInfra = InternInfra.This;
        this.Intern = Extern.Storage_New();
        Extern.Storage_Init(this.Intern);
        return true;
    }

    public virtual bool Final()
    {
        Extern.Storage_Final(this.Intern);
        Extern.Storage_Delete(this.Intern);
        return true;
    }

    public virtual string Path { get; set; }
    public virtual Mode Mode { get; set; }
    public virtual StreamStream Stream { get; set; }
    protected virtual StreamStream DataStream { get; set; }

    private InternIntern InternIntern { get; set; }
    private InternInfra InternInfra { get; set; }
    private ulong Intern { get; set; }
    private ulong InternPath { get; set; }

    public virtual int Status
    {
        get
        {
            ulong u;
            u = Extern.Storage_GetStatus(this.Intern);
            int o;
            o = (int)u;
            return o;
        }
        set
        {
        }
    }

    public virtual bool Open()
    {
        if (!(this.Stream == null))
        {
            return true;
        }
        if (!this.CheckMode(this.Mode))
        {
            return true;
        }

        this.InternPath = this.InternInfra.StringCreate(this.Path);
        ulong modeU;
        modeU = this.GetInternMode(this.Mode);
        this.DataStream = this.CreateStream();

        Extern.Storage_SetPath(this.Intern, this.InternPath);
        Extern.Storage_SetMode(this.Intern, modeU);
        Extern.Storage_SetStream(this.Intern, this.DataStream.Ident);
        Extern.Storage_Open(this.Intern);
        if (this.Status == 0)
        {
            this.Stream = this.DataStream;
        }
        return true;
    }

    public virtual bool Close()
    {
        Extern.Storage_Close(this.Intern);
        Extern.Storage_SetStream(this.Intern, 0);
        Extern.Storage_SetMode(this.Intern, 0);
        Extern.Storage_SetPath(this.Intern, 0);

        this.DataStream.Final();
        this.DataStream = null;
        this.Stream = null;

        this.InternInfra.StringDelete(this.InternPath);
        return true;
    }

    protected virtual bool CheckMode(Mode mode)
    {
        if (!(mode.Read | mode.Write))
        {
            return false;
        }
        if (mode.New & mode.Existing)
        {
            return false;
        }
        return true;
    }

    protected virtual StreamStream CreateStream()
    {
        Stream a;
        a = new Stream();
        a.Init();
        StreamStream o;
        o = a;
        return o;
    }

    private ulong GetInternMode(Mode mode)
    {
        ulong share;
        share = Extern.Infra_Share();
        ulong stat;
        stat = Extern.Share_Stat(share);

        ulong k;
        k = 0;
        if (mode.Read)
        {
            k = k | Extern.Stat_StorageModeRead(stat);
        }
        if (mode.Write)
        {
            k = k | Extern.Stat_StorageModeWrite(stat);
        }
        if (mode.New)
        {
            k = k | Extern.Stat_StorageModeNew(stat);
        }
        if (mode.Existing)
        {
            k = k | Extern.Stat_StorageModeExisting(stat);
        }
        return k;
    }
}