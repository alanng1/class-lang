namespace Class.Infra;

public class Module : Any
{
    public virtual ModuleRef Ref { get; set; }

    public virtual Table Class { get; set; }

    public virtual Table Import { get; set; }

    public virtual object Any { get; set; }
}