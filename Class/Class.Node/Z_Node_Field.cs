namespace Class.Node;

public class Field : Comp
{
    public virtual ClassName Class { get; set; }


    public virtual FieldName Name { get; set; }


    public virtual Access Access { get; set; }


    public virtual State Get { get; set; }


    public virtual State Set { get; set; }
}