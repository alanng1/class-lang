namespace Avalon.Text;

public class Format : Any
{
    public override bool Init()
    {
        base.Init();
        this.InternIntern = InternIntern.This;
        this.InternInfra = InternInfra.This;
        this.InfraInfra = InfraInfra.This;
        this.TextInfra = Infra.This;
        
        this.InternBase = Extern.String_New();
        Extern.String_Init(this.InternBase);

        this.InternResult = Extern.String_New();
        Extern.String_Init(this.InternResult);

        this.Intern = Extern.Format_New();
        Extern.Format_Init(this.Intern);

        return true;
    }

    public virtual bool Final()
    {
        Extern.Format_Final(this.Intern);
        Extern.Format_Delete(this.Intern);

        Extern.String_Final(this.InternResult);
        Extern.String_Delete(this.InternResult);

        Extern.String_Final(this.InternBase);
        Extern.String_Delete(this.InternBase);

        return true;
    }

    private InternIntern InternIntern { get; set; }

    private InternInfra InternInfra { get; set; }

    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual Infra TextInfra { get; set; }

    private ulong Intern { get; set; }

    private ulong InternResult { get; set; }

    private ulong InternBase { get; set; }


    public virtual int ExecuteCount(Span varBase, FormatArgList argList)
    {
        InfraRange range;
        range = varBase.Range;
        int baseIndex;
        baseIndex = range.Index;
        int baseCount;
        baseCount = range.Count;

        ulong baseIndexU;
        baseIndexU = (ulong)baseIndex;
        ulong baseCountU;
        baseCountU = (ulong)baseCount;

        int a;
        a = this.InternIntern.FormatCount(this.Intern, varBase.Data.Value, baseIndexU, baseCountU, this.InternBase, argList.Intern);
        return a;
    }


    public virtual bool ExecuteResult(Span varBase, FormatArgList argList, Span result)
    {
        InfraRange baseRange;
        baseRange = varBase.Range;
        int baseIndex;
        baseIndex = baseRange.Index;
        int baseCount;
        baseCount = baseRange.Count;
        InfraRange resultRange;
        resultRange = result.Range;
        int resultIndex;
        resultIndex = resultRange.Index;
        int resultCount;
        resultCount = resultRange.Count;

        ulong baseIndexU;
        baseIndexU = (ulong)baseIndex;
        ulong baseCountU;
        baseCountU = (ulong)baseCount;
        ulong resultIndexU;
        resultIndexU = (ulong)resultIndex;
        ulong resultCountU;
        resultCountU = (ulong)resultCount;

        this.InternIntern.FormatResult(this.Intern, varBase.Data.Value, baseIndexU, baseCountU, this.InternBase, argList.Intern, 
            result.Data.Value, resultIndexU, resultCountU, this.InternResult);

        return true;
    }

    public virtual bool ExecuteArgCount(FormatArg arg)
    {
        Extern.Format_ExecuteArgCount(this.Intern, arg.Intern);
        return true;
    }

    public virtual bool ExecuteArgResult(FormatArg arg, Span result)
    {
        InfraRange range;
        range = result.Range;
        int resultIndex;
        resultIndex = range.Index;
        int resultCount;
        resultCount = range.Count;

        ulong resultIndexU;
        resultIndexU = (ulong)resultIndex;
        ulong resultCountU;
        resultCountU = (ulong)resultCount;

        this.InternIntern.FormatArgResult(this.Intern, arg.Intern, result.Data.Value, resultIndexU, resultCountU, this.InternResult);

        return true;
    }
}