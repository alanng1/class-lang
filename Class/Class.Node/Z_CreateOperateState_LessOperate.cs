namespace Class.Node;

public class LessOperateCreateOperateState : CreateOperateState
{
    public override bool Execute()
    {
        LessOperate node;
        node = (LessOperate)this.Node;
        node.Left = (Operate)this.Arg.Field00;
        node.Right = (Operate)this.Arg.Field01;
        return true;
    }
}