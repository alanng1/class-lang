namespace Avalon.View;

public class Grid : View
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.RowField = this.CreateRowField();
        this.ColField = this.CreateColField();
        this.ChildField = this.CreateGridChildField();
        this.DestField = this.CreateDestField();
        this.Row = this.CreateRow();
        this.Col = this.CreateCol();
        this.Child = this.CreateChild();
        this.Dest = this.CreateDest();
        this.RangeA = this.CreateRangeA();
        this.ChildPosList = this.CreateChildPosList();
        this.RowIter = this.Row.CreateIter();
        this.ColIter = this.Col.CreateIter();
        this.ChildIter = this.Child.CreateIter();

        this.StackGridChildListRect = this.CreateStackGridChildListRect();
        this.StackGridChildListPos = this.CreateStackGridChildListPos();
        this.StackGridChildRect = this.CreateStackGridChildRect();
        this.StackGridChildPos = this.CreateStackGridChildPos();

        this.DataRead = new DataRead();
        this.DataRead.Init();
        this.DataRead.Data = this.ChildPosList;
        this.DataWrite = new DataWrite();
        this.DataWrite.Init();
        this.DataWrite.Data = this.ChildPosList;
        return true;
    }

    protected virtual Iter RowIter { get; set; }
    protected virtual Iter ColIter { get; set; }
    protected virtual Iter ChildIter { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual DataRead DataRead { get; set; }
    protected virtual DataWrite DataWrite { get; set; }
    protected virtual InfraRange RangeA { get; set; }

    protected virtual DrawRect StackGridChildListRect { get; set; }
    protected virtual DrawPos StackGridChildListPos { get; set; }
    protected virtual DrawRect StackGridChildRect { get; set; }
    protected virtual DrawPos StackGridChildPos { get; set; }

    protected virtual Field CreateRowField()
    {
        return this.ViewInfra.FieldCreate(this);
    }
    
    protected virtual Field CreateColField()
    {
        return this.ViewInfra.FieldCreate(this);
    }

    protected virtual Field CreateGridChildField()
    {
        return this.ViewInfra.FieldCreate(this);
    }

    protected virtual Field CreateDestField()
    {
        return this.ViewInfra.FieldCreate(this);
    }

    protected virtual List CreateRow()
    {
        List a;
        a = new List();
        a.Init();
        return a;
    }

    protected virtual List CreateCol()
    {
        List a;
        a = new List();
        a.Init();
        return a;
    }

    protected virtual List CreateChild()
    {
        List a;
        a = new List();
        a.Init();
        return a;
    }

    protected virtual Rect CreateDest()
    {
        Rect a;
        a = new Rect();
        a.Init();
        return a;
    }

    protected virtual InfraRange CreateRangeA()
    {
        InfraRange a;
        a = new InfraRange();
        a.Init();
        return a;
    }

    protected virtual Data CreateChildPosList()
    {
        Data a;
        a = new Data();
        a.Count = 0;
        a.Init();
        return a;
    }

    protected virtual DrawRect CreateStackGridChildRect()
    {
        DrawRect rect;
        rect = new DrawRect();
        rect.Init();
        rect.Pos = new DrawPos();
        rect.Pos.Init();
        rect.Size = new DrawSize();
        rect.Size.Init();
        return rect;
    }

    protected virtual DrawPos CreateStackGridChildPos()
    {
        DrawPos pos;
        pos = new DrawPos();
        pos.Init();
        return pos;
    }

    protected virtual DrawRect CreateStackGridChildListRect()
    {
        DrawRect rect;
        rect = new DrawRect();
        rect.Init();
        rect.Pos = new DrawPos();
        rect.Pos.Init();
        rect.Size = new DrawSize();
        rect.Size.Init();
        return rect;
    }

    protected virtual DrawPos CreateStackGridChildListPos()
    {
        DrawPos pos;
        pos = new DrawPos();
        pos.Init();
        return pos;
    }

    public virtual Field RowField { get; set; }

    public virtual List Row
    {
        get
        {
            return (List)this.RowField.Get();
        }

        set
        {
            this.RowField.Set(value);
        }
    }

    protected virtual bool ChangeRow(Change change)
    {
        if ((this.Row == null) | (this.Col == null) | (this.Child == null))
        {
            return true;
        }
        this.UpdateLayout();
        this.Trigger(this.RowField);
        return true;
    }

    public virtual Field ColField { get; set; }

    public virtual List Col
    {
        get
        {
            return (List)this.ColField.Get();
        }

        set
        {
            this.ColField.Set(value);
        }
    }

    protected virtual bool ChangeCol(Change change)
    {
        if ((this.Row == null) | (this.Col == null) | (this.Child == null))
        {
            return true;
        }
        this.UpdateLayout();
        this.Trigger(this.ColField);
        return true;
    }

    public new virtual Field ChildField { get; set; }

    public new virtual List Child
    {
        get
        {
            return (List)this.ChildField.Get();
        }

        set
        {
            this.ChildField.Set(value);
        }
    }

    protected new virtual bool ChangeChild(Change change)
    {
        if ((this.Row == null) | (this.Col == null) | (this.Child == null))
        {
            return true;
        }
        this.Trigger(this.ChildField);
        return true;
    }

    public virtual Field DestField { get; set; }

    public virtual Rect Dest
    {
        get
        {
            return (Rect)this.DestField.Get();
        }
        set
        {
            this.DestField.Set(value);
        }
    }

    protected virtual bool ChangeDest(Change change)
    {
        this.Trigger(this.DestField);
        return true;
    }

    protected virtual bool UpdateLayout()
    {
        int count;
        count = this.Col.Count + this.Row.Count;

        int oa;
        oa = count * sizeof(int);
        long oo;
        oo = this.ChildPosList.Count;
        int ob;
        ob = (int)oo;
        if (ob < oa)
        {
            Data data;
            data = new Data();
            data.Count = oa;
            data.Init();
            this.ChildPosList = data;
            this.DataRead.Data = this.ChildPosList;
            this.DataWrite.Data = this.ChildPosList;
        }
        
        this.SetChildLeftArray();
        this.SetChildUpArray();
        return true;
    }

    protected override bool ExecuteDrawThis(DrawDraw draw)
    {
        return true;
    }

    protected override bool CheckDrawChild()
    {
        return true;
    }

    protected override bool ExecuteChildDraw(DrawDraw draw)
    {
        this.ExecuteDrawGridChildList(draw);
        return true;
    }

    protected virtual bool ExecuteDrawGridChildList(DrawDraw draw)
    {
        int left;
        left = this.Dest.Pos.Left;
        left = left + draw.Pos.Left;
        int up;
        up = this.Dest.Pos.Up;
        up = up + draw.Pos.Up;
        int width;
        width = this.Dest.Size.Width;
        int height;
        height = this.Dest.Size.Height;

        this.DrawRectA.Pos.Left = left;
        this.DrawRectA.Pos.Up = up;
        this.DrawRectA.Size.Width = width;
        this.DrawRectA.Size.Height = height;

        this.SetChildArea(this.DrawRectA);

        this.ViewInfra.StackPushChild(draw, this.StackGridChildListRect, this.StackGridChildListPos, this.DrawRectA, this.DrawPosA);

        this.ExecuteGridChildListDraw(draw);

        this.ViewInfra.StackPopChild(draw, this.StackGridChildListRect, this.StackGridChildListPos);
        return true;
    }

    protected virtual bool ExecuteGridChildListDraw(DrawDraw draw)
    {
        Iter iter;
        iter = this.ChildIter;
        this.Child.SetIter(iter);
        while (iter.Next())
        {
            GridChild child;
            child = (GridChild)iter.Value;

            if (this.CheckDrawGridChild(child))
            {
                this.ExecuteDrawGridChild(draw, child);
            }
        }
        return true;
    }

    protected virtual bool CheckDrawGridChild(GridChild child)
    {
        return !(child.View == null);
    }

    protected virtual bool ExecuteDrawGridChild(DrawDraw draw, GridChild child)
    {
        GridRect gridRect;
        gridRect = child.Rect;

        if (!this.CheckGridRect(gridRect))
        {
            return true;
        }

        GridPos gridPos;
        gridPos = gridRect.Pos;
        GridSize gridSize;
        gridSize = gridRect.Size;

        int startCol;
        startCol = gridPos.Col;
        int endCol;
        endCol = startCol + gridSize.Width;
        int startRow;
        startRow = gridPos.Row;
        int endRow;
        endRow = startRow + gridSize.Height;

        int leftA;
        leftA = this.GridColLeft(startCol);
        int upA;
        upA = this.GridRowUp(startRow);
        int left;
        left = leftA + draw.Pos.Left;
        int up;
        up = upA + draw.Pos.Up;
        
        int right;
        right = this.GridColLeft(endCol);
        int down;
        down = this.GridRowUp(endRow);

        int width;
        width = right - leftA;
        int height;
        height = down - upA;

        this.DrawRectA.Pos.Left = left;
        this.DrawRectA.Pos.Up = up;
        this.DrawRectA.Size.Width = width;
        this.DrawRectA.Size.Height = height;

        this.SetChildArea(this.DrawRectA);

        this.ViewInfra.StackPushChild(draw, this.StackGridChildRect, this.StackGridChildPos, this.DrawRectA, this.DrawPosA);

        this.ExecuteGridChildDraw(draw, child);

        this.ViewInfra.StackPopChild(draw, this.StackGridChildRect, this.StackGridChildPos);
        return true;
    }

    protected virtual bool ExecuteGridChildDraw(DrawDraw draw, GridChild child)
    {
        child.View.ExecuteDraw(draw);
        return true;
    }

    protected virtual bool CheckGridRect(GridRect rect)
    {
        GridPos pos;
        pos = rect.Pos;
        GridSize size;
        size = rect.Size;

        this.RangeA.Index = pos.Col;
        this.RangeA.Count = size.Width;
        bool ba;
        ba = this.InfraInfra.CheckRange(this.Col.Count, this.RangeA);

        this.RangeA.Index = pos.Row;
        this.RangeA.Count = pos.Row + size.Height;
        bool bb;
        bb = this.InfraInfra.CheckRange(this.Row.Count, this.RangeA);

        bool a;
        a = ba & bb;
        return a;
    }

    protected virtual int GridColLeft(int col)
    {
        return this.GridPosPixelPos(col, 0);
    }

    protected virtual int GridRowUp(int row)
    {
        return this.GridPosPixelPos(row, this.Col.Count);
    }

    protected virtual int GridPosPixelPos(int pos, int start)
    {
        int t;
        t = pos;
        bool b;
        int u;
        u = 0;

        b = (t < 1);
        if (!b)
        {
            t = t - 1;
            int index;
            index = start + t;
            int byteIndex;
            byteIndex = this.IntByteIndex(index);

            u = this.DataRead.ExecuteMid(byteIndex);
        }
        int ret;
        ret = u;
        return ret;
    }

    protected virtual bool SetChildLeftArray()
    {
        int start;
        start = 0;
        Iter iter;
        iter = this.ColIter;
        this.Col.SetIter(iter);
        int left;
        left = 0;

        int i;
        i = 0;
        while (iter.Next())
        {
            GridCol gridCol;
            gridCol = (GridCol)iter.Value;
            left = left + gridCol.Width;

            int index;
            index = start + i;
            int byteIndex;
            byteIndex = this.IntByteIndex(index);
            this.DataWrite.ExecuteMid(byteIndex, left);
            i = i + 1;
        }
        return true;
    }

    protected virtual bool SetChildUpArray()
    {
        int start;
        start = this.Col.Count;
        Iter iter;
        iter = this.RowIter;
        this.Row.SetIter(iter);
        int up;
        up = 0;

        int i;
        i = 0;
        while (iter.Next())
        {
            GridRow gridRow;
            gridRow = (GridRow)iter.Value;
            up = up + gridRow.Height;

            int index;
            index = start + i;
            int byteIndex;
            byteIndex = this.IntByteIndex(index);
            this.DataWrite.ExecuteMid(byteIndex, up);
            i = i + 1;
        }
        return true;
    }

    protected virtual Data ChildPosList { get; set; }

    public override bool Change(Field field, Change change)
    {
        base.Change(field, change);
        if (this.RowField == field)
        {
            this.ChangeRow(change);
        }
        if (this.ColField == field)
        {
            this.ChangeCol(change);
        }
        if (this.ChildField == field)
        {
            this.ChangeChild(change);
        }
        return true;
    }

    protected virtual int IntByteIndex(int index)
    {
        return index * sizeof(int);
    }
}