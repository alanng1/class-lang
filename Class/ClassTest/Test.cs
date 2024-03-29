namespace Class.Test;

public class Test : Any
{
    public override bool Init()
    {
        base.Init();


        this.InfraInfra = InfraInfra.This;
        this.TaskKindList = TaskKindList.This;



        this.LanguageName = this.LanguageNameCreate();




        this.UnitIndex = 0;




        this.PassCount = 0;






        this.DataFold = ".";






        this.ResultSpace = new string(' ', 4);


        

        
        this.Class = this.CreateClass();






        string oo = this.DataRootDirectory();



        Directory.SetCurrentDirectory(oo);





        this.OriginalCurrentDirectory = Directory.GetCurrentDirectory();








        StringCompare compare;


        compare = new StringCompare();


        compare.Init();





        this.SetMap = new Table();



        this.SetMap.Compare = compare;



        this.SetMap.Init();





        this.AddSetList();




        return true;
    }

    protected virtual InfraInfra InfraInfra { get; set; }

    protected virtual TaskKindList TaskKindList { get; set; }

    protected virtual string LanguageName { get; set; }

    private string DataFold { get; set; }

    private List UnitList { get; set; }

    private Table SetMap { get; set; }

    private Set Set { get; set; }

    private Unit Unit { get; set; }

    private int PassCount { get; set; }

    private ClassClass Class { get; set; }

    private string UnitFold { get; set; }

    private Exception Error { get; set; }

    private StringOut Out { get; set; }

    private string OriginalCurrentDirectory { get; set; }

    private string ResultSpace { get; set; }

    private int UnitIndex { get; set; }

    private bool UnitPass { get; set; }

    protected virtual string DataRootDirectory()
    {
        return "../../Class/ClassTest/Test";
    }

    protected virtual bool AddSetList()
    {
        this.AddSet("Token", this.TaskKindList.Token, false, false, false);

        this.AddSet("Node", this.TaskKindList.Node, true, false, false);
        return true;
    }






    protected bool AddSet(string name, TaskKind taskKind, bool addKindAfterTaskArg, bool addPathAfterTaskArg, bool sourceFold)
    {
        Set set;




        set = new Set();





        set.Name = name;





        set.TaskKind = taskKind;






        set.AddKindAfterTaskArg = addKindAfterTaskArg;





        set.AddPathAfterTaskArg = addPathAfterTaskArg;





        set.SourceFold = sourceFold;






        TableEntry pair;


        pair = new TableEntry();


        pair.Init();


        pair.Index = set.Name;


        pair.Value = set;




        this.SetMap.Add(pair);



        return true;
    }

    public virtual int Execute()
    {
        this.ExecuteSetList();



        return 0;
    }

    



    protected virtual bool ExecuteSetList()
    {
        Iter iter;

        iter = this.SetMap.IterCreate();


        this.SetMap.IterSet(iter);



        while (iter.Next())
        {
            this.Set = (Set)iter.Value;




            this.AddSetUnitList();



            this.ExecuteSet();



            this.Set = null;
        }



        return true;
    }



    private bool AddSetUnitList()
    {
        this.UnitList = new List();



        this.UnitList.Init();






        string set;

        set = this.Set.Name;

        
        

        string setFold;
            
        setFold = this.DataFold + this.InfraInfra.PathCombine + set;





        Array kindList;

        kindList = this.GetFoldList(setFold);



        Iter kindIter;

        kindIter = kindList.IterCreate();


        kindList.IterSet(kindIter);

    

        while (kindIter.Next())
        {
            string kind;

            kind = (string)kindIter.Value;




            string kindFold;


            kindFold = setFold + this.InfraInfra.PathCombine + kind;




            Array unitList;
            
            unitList = this.GetFoldList(kindFold);





            Iter unitIter;

            unitIter = unitList.IterCreate();


            unitList.IterSet(unitIter);




            while (unitIter.Next())
            {
                string unit;


                unit = (string)unitIter.Value;




                string unitFold;
                

                unitFold = kindFold + this.InfraInfra.PathCombine + unit;




                string expectFile;
                
                
                expectFile = unitFold + this.InfraInfra.PathCombine + "Expect";


                


                string expect;
                
                
                
                expect = File.ReadAllText(expectFile);






                string path;



                path = null;



                if (this.Set.AddPathAfterTaskArg)
                {
                    string pathFile;


                    pathFile = unitFold + this.InfraInfra.PathCombine + "Path";




                    path = File.ReadAllText(pathFile);
                }
                




                Unit a;


                a = new Unit();


                a.Set = this.Set;


                a.Kind = kind;


                a.Name = unit;


                a.Expect = expect;



                a.Path = path;




                this.UnitList.Add(a);
            }
        }




        return true;
    }




    private bool ExecuteSet()
    {
        this.WriteHeader(this.Set.Name);





        this.PassCount = 0;



        this.UnitIndex = 0;




        Iter iter;

        iter = this.UnitList.IterCreate();
        

        this.UnitList.IterSet(iter);



        while (iter.Next())
        {
            this.Unit = (Unit)iter.Value;

            this.ExecuteUnit();

            this.WriteUnitResult();

            this.Unit = null;

            this.UnitIndex = this.UnitIndex + 1;

            if (this.UnitPass)
            {
                this.PassCount = this.PassCount + 1;
            }
        }

        this.WriteTotalResult();
        return true;
    }

    private bool ExecuteUnit()
    {
        char oc;
        oc = this.InfraInfra.PathCombine;

        this.UnitFold = this.DataFold + oc + this.Unit.Set.Name + oc + this.Unit.Kind + oc + this.Unit.Name;



        this.Out = new StringOut();
        this.Out.Init();




        Directory.SetCurrentDirectory(this.UnitFold);







        Task task;



        task = this.CreateTask();



        this.Class.Task = task;
    


        this.Class.Execute();




        Directory.SetCurrentDirectory(this.OriginalCurrentDirectory);





        string actual;
        

        actual = this.Out.Result();




        string actualFile;
        

        actualFile = this.UnitFold + oc + "Actual";




        File.WriteAllText(actualFile, actual);



        this.Unit.Actual = actual;





        bool pass;

        pass = (this.Unit.Actual == this.Unit.Expect);




        this.UnitPass = pass;




        return true;
    }





    protected virtual string LanguageNameCreate()
    {
        return "Class";
    }






    private bool WriteUnitResult()
    {
        this.WriteResultLine(this.UnitPass, this.Unit.Set.Name, this.Unit.Kind, this.Unit.Name);



        return true;
    }






    private bool WriteResultLine(bool pass, string set, string kind, string unit)
    {
        string a;



        a = null;




        bool b;


        b = pass;



        if (b)
        {
            a = "Pass";
        }
        


        if (!b)
        {
            a = "Fail";
        }






        string u;

        u = string.Format("{0,-8}", set);




        string k;

        k = string.Format("{0,-24}", kind);




        string j;


        j = unit;





        int number;


        number = this.UnitIndex + 1;




        string p;


        p = number.ToString("D3");





        string s;



        s = p + this.ResultSpace + a + this.ResultSpace + u + this.ResultSpace + k + " " + j + "\n";




        SystemConsole.Write(s);




        return true;
    }






    private bool WriteTotalResult()
    {
        string o;
        o = null;

        int unitCount;
        unitCount = this.UnitIndex;
        
        bool b;
        b = (this.PassCount == unitCount);
        if (b)
        {
            o = "All";
        }
        if (!b)
        {
            o = this.PassCount.ToString();
        }

        SystemConsole.Write(o + " " + "Pass" + "\n");
        return true;
    }





    private bool WriteHeader(string setName)
    {
        string s;


        s = this.LanguageName.ToUpper();




        string k;


        k = setName.ToUpper();




        SystemConsole.Write("==============================" + " " + s + " " + k + " " + "TEST" + " " + "===============================" + "\n");



        return true;
    }

    private Task CreateTask()
    {
        Task task;
        task = new Task();
        task.Init();
        task.Kind = this.Unit.Set.TaskKind;

        bool ba;
        ba = this.Set.AddKindAfterTaskArg;
        if (ba)
        {
            task.Node = this.Unit.Kind;
        }
        if (!ba)
        {
            task.Node = "Class";
        }

        if (this.Set.AddPathAfterTaskArg)
        {
            task.Check = this.Unit.Path;
        }

        task.Print = true;

        string source;
        source = null;
        bool b;
        b = this.Set.SourceFold;
        if (b)
        {
            source = "Source";
        }
        if (!b)
        {
            source = "Code";
        }
        task.Source = source;

        task.Out = this.Out;

        Task ret;
        ret = task;
        return ret;
    }







    protected virtual ClassClass CreateClass()
    {
        ClassClass t;



        t = new ClassClass();



        t.Init();





        ClassClass ret;


        ret = t;


        return ret;
    }







    private Array GetFoldList(string foldPath)
    {
        string[] u;
            
            
        u = Directory.GetDirectories(foldPath);




        int count;

        count = u.Length;


        int i;

        i = 0;


        while (i < count)
        {
            string path;

            path = u[i];



            string name;

            name = Path.GetFileName(path);



            u[i] = name;



            i = i + 1;
        }





        Array array;

        array = new Array();

        array.Count = u.Length;

        array.Init();




        count = array.Count;



        i = 0;



        while (i < count)
        {
            string k;

            k = u[i];


            array.Set(i, k);


            i = i + 1;
        }


        Range range;
        range = new Range();
        range.Init();
        range.Count = count;

        StringCompare compare;
        compare = new StringCompare();
        compare.Init();

        array.Sort(range, compare);


        return array;
    }
}