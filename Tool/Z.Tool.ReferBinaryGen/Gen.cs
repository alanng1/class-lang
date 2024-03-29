namespace Z.Tool.ReferBinaryGen;

public class Gen : Any
{
    public override bool Init()
    {
        base.Init();

        this.Main = new Main();
        this.Main.Init();

        this.ListInfra = ListInfra.This;
        this.StorageInfra = StorageInfra.This;
        this.CountList = CountList.This;

        Table table;
        table = new Table();
        table.Compare = new RefCompare();
        table.Compare.Init();
        table.Init();

        this.DotNetBuiltInTypeTable = table;

        this.AddDotNetBuiltInType(typeof(object), "Any", 0);
        this.AddDotNetBuiltInType(typeof(bool), "Bool", 0);
        this.AddDotNetBuiltInType(typeof(long), "Int", 1);
        this.AddDotNetBuiltInType(typeof(ulong), "Int", 2);
        this.AddDotNetBuiltInType(typeof(int), "Int", 3);
        this.AddDotNetBuiltInType(typeof(uint), "Int", 4);
        this.AddDotNetBuiltInType(typeof(short), "Int", 5);
        this.AddDotNetBuiltInType(typeof(ushort), "Int", 6);
        this.AddDotNetBuiltInType(typeof(sbyte), "Int", 7);
        this.AddDotNetBuiltInType(typeof(byte), "Int", 8);
        this.AddDotNetBuiltInType(typeof(char), "Int", 9);
        this.AddDotNetBuiltInType(typeof(string), "String", 0);

        this.CountArray = this.ListInfra.ArrayCreate(4);
        this.SetCountString("Prudate");
        this.SetCountString("Probate");
        this.SetCountString("Precate");
        this.SetCountString("Private");
        return true;
    }

    public virtual bool Final()
    {
        this.Main.Final();
        return true;
    }

    protected virtual ListInfra ListInfra { get; set; }
    protected virtual StorageInfra StorageInfra { get; set; }
    protected virtual CountList CountList { get; set; }
    protected virtual Table ModuleTable { get; set; }
    protected virtual Module Module { get; set; }
    protected virtual Table DotNetBuiltInTypeTable { get; set; }
    protected virtual Table ReferTable { get; set; }
    protected virtual Array CountArray { get; set; }
    protected virtual ClassClass AnyClass { get; set; }
    protected virtual bool IsAvalonInfra { get; set; }
    protected virtual int Index { get; set; }
    protected virtual Main Main { get; set; }

    public virtual int Execute()
    {
        this.ModuleTable = new Table();
        this.ModuleTable.Compare = new StringCompare();
        this.ModuleTable.Compare.Init();
        this.ModuleTable.Init();

        this.ExecuteModule(typeof(Any).Assembly);
        this.ExecuteModule(typeof(ListList).Assembly);
        this.ExecuteModule(typeof(Math).Assembly);
        this.ExecuteModule(typeof(Text).Assembly);
        this.ExecuteModule(typeof(Event).Assembly);
        this.ExecuteModule(typeof(Comp).Assembly);
        this.ExecuteModule(typeof(Thread).Assembly);
        this.ExecuteModule(typeof(Stream).Assembly);
        this.ExecuteModule(typeof(Type).Assembly);
        this.ExecuteModule(typeof(Time).Assembly);
        this.ExecuteModule(typeof(Video).Assembly);
        this.ExecuteModule(typeof(Effect).Assembly);
        this.ExecuteModule(typeof(Storage).Assembly);
        this.ExecuteModule(typeof(Network).Assembly);
        this.ExecuteModule(typeof(Console).Assembly);
        this.ExecuteModule(typeof(Play).Assembly);
        this.ExecuteModule(typeof(Draw).Assembly);
        this.ExecuteModule(typeof(View).Assembly);
        this.ExecuteModule(typeof(Main).Assembly);
        this.ExecuteModule(typeof(EntryEntry).Assembly);
        this.ExecuteModule(typeof(ClassClass).Assembly);
        this.ExecuteModule(typeof(Refer).Assembly);
        this.ExecuteModule(typeof(Port).Assembly);
        this.ExecuteModule(typeof(Token).Assembly);
        this.ExecuteModule(typeof(Node).Assembly);
        this.ExecuteModule(typeof(Check).Assembly);
        this.ExecuteModule(typeof(ModuleResult).Assembly);
        this.ExecuteModule(typeof(Task).Assembly);

        //this.ConsoleWrite();

        ReferGen referGen;
        referGen = new ReferGen();
        referGen.Init();
        referGen.ModuleTable = this.ModuleTable;
        referGen.DotNetBuiltInTypeTable = this.DotNetBuiltInTypeTable;

        referGen.Execute();

        this.ReferTable = referGen.ReferTable;

        this.ExecuteReferWrite();

        return 0;
    }

    protected virtual bool ExecuteModule(Assembly assembly)
    {
        ModuleRef oa;
        oa = new ModuleRef();
        oa.Init();
        oa.Name = assembly.GetName().Name;

        Module module;
        module = new Module();
        module.Init();
        module.Ref = oa;
        module.Any = assembly;
        this.Module = module;

        this.ListInfra.TableAdd(this.ModuleTable, module.Ref.Name, module);

        this.IsAvalonInfra = (this.Module.Ref.Name == "Avalon.Infra");

        this.SetClassList();

        this.SetImportList();

        return true;
    }

    protected virtual bool SetClassList()
    {
        Table table;
        table = new Table();
        table.Compare = new StringCompare();
        table.Compare.Init();
        table.Init();
        
        this.Module.Class = table;

        this.AddClassList();

        this.AddBaseList();

        this.AddPartList();
        return true;
    }

    protected virtual bool AddClassList()
    {
        Assembly assembly;
        assembly = (Assembly)this.Module.Any;
        SystemType[] typeArray;
        typeArray = assembly.GetExportedTypes();

        SystemType anyType;
        anyType = null;
        bool b;
        b = this.IsAvalonInfra;
        if (b)
        {
            anyType = this.AnyType(typeArray);

            if (anyType == null)
            {
                global::System.Console.Error.Write("Any class not found\n");
                global::System.Environment.Exit(103);
            }

            this.AddClass(anyType);

            this.AddInfraBuiltInClassList();
        }

        int count;
        count = typeArray.Length;
        int i;
        i = 0;
        while (i < count)
        {
            SystemType type;
            type = typeArray[i];

            if (!(b & (type == anyType)))
            {
                this.AddClass(type);
            }

            i = i + 1;
        }
        return true;
    }

    protected virtual bool AddBaseList()
    {
        if (this.IsAvalonInfra)
        {
            ClassClass anyClass;
            anyClass = this.ModuleClassGet(this.Module, "Any");
            this.AnyClass = anyClass;

            anyClass.Base = anyClass;

            this.ClassBaseSetAny("Bool");
            this.ClassBaseSetAny("Int");
            this.ClassBaseSetAny("String");
        }

        Iter iter;
        iter = this.Module.Class.IterCreate();
        this.Module.Class.IterSet(iter);
        while (iter.Next())
        {
            ClassClass a;
            a = (ClassClass)iter.Value;

            if (a.Base == null)
            {
                SystemType oa;
                oa = (SystemType)a.Any;
                SystemType ob;
                ob = oa.BaseType;

                ClassClass oc;
                oc = this.ClassGetType(ob);
                a.Base = oc;
            }
        }
        return true;
    }

    protected virtual bool AddPartList()
    {
        if (this.IsAvalonInfra)
        {
            this.ClassPartSetEmpty("Bool");
            this.ClassPartSetEmpty("Int");
            this.ClassPartSetEmpty("String");
        }

        Iter iter;
        iter = this.Module.Class.IterCreate();
        this.Module.Class.IterSet(iter);
        while (iter.Next())
        {
            ClassClass a;
            a = (ClassClass)iter.Value;

            if (a.Field == null)
            {
                this.AddPart(a);
            }
        }

        return true;
    }

    protected virtual bool AddClass(SystemType type)
    {
        ClassClass a;
        a = new ClassClass();
        a.Init();

        a.Index = this.Module.Class.Count;
        a.Name = type.Name;
        a.Module = this.Module;

        a.Any = type;

        this.ListInfra.TableAdd(this.Module.Class, a.Name, a);

        return true;
    }

    protected virtual bool AddPart(ClassClass varClass)
    {
        SystemType type;
        type = (SystemType)varClass.Any;

        PropertyInfo[] propertyArrayA;
        propertyArrayA = type.GetProperties(BindingFlag.Instance | BindingFlag.Public | BindingFlag.NonPublic | BindingFlag.DeclaredOnly | BindingFlag.ExactBinding);

        MethodInfo[] methodArrayA;
        methodArrayA = type.GetMethods(BindingFlag.Instance | BindingFlag.Public | BindingFlag.NonPublic | BindingFlag.DeclaredOnly | BindingFlag.ExactBinding);

        int count;
        int i;
        Table fieldTable;
        fieldTable = this.TableCreate();

        count = propertyArrayA.Length;
        i = 0;
        while (i < count)
        {
            PropertyInfo property;
            property = propertyArrayA[i];

            if (!property.IsSpecialName & property.CanRead & property.CanWrite)
            {
                if (this.IsInAbstract(property.GetMethod) & !((type == typeof(Data)) & (property.Name == "Value")))
                {
                    Field field;
                    field = new Field();
                    field.Init();
                    field.Name = property.Name;
                    field.Class = this.ClassGetType(property.PropertyType);
                    field.Count = this.CountGet(property.GetMethod);
                    field.Any = property;
                    this.ListInfra.TableAdd(fieldTable, field.Name, field);
                }
            }

            i = i + 1;
        }

        Table maideTable;
        maideTable = this.TableCreate();

        count = methodArrayA.Length;
        i = 0;
        while (i < count)
        {
            MethodInfo method;
            method = methodArrayA[i];

            if (!method.IsSpecialName & this.IsInAbstract(method) & !((type == typeof(EntryEntry)) & (method.Name == "ArgSet")))
            {
                Maide maide;
                maide = new Maide();
                maide.Init();
                maide.Name = method.Name;
                maide.Class = this.ClassGetType(method.ReturnType);
                maide.Count = this.CountGet(method);
                maide.Any = method;

                ParameterInfo[] parameterArray;
                parameterArray = method.GetParameters();
                int countA;
                countA = parameterArray.Length;

                Table varTable;
                varTable = this.TableCreate();

                int iA;
                iA = 0;
                while (iA < countA)
                {
                    ParameterInfo parameter;
                    parameter = parameterArray[iA];
                    Var varVar;
                    varVar = new Var();
                    varVar.Init();
                    varVar.Name = parameter.Name;
                    varVar.Class = this.ClassGetType(parameter.ParameterType);
                    varVar.Any = parameter;

                    this.ListInfra.TableAdd(varTable, varVar.Name, varVar);

                    iA = iA + 1;
                }
                maide.Param = varTable;

                this.ListInfra.TableAdd(maideTable, maide.Name, maide);
            }

            i = i + 1;
        }

        varClass.Field = fieldTable;
        varClass.Maide = maideTable;

        return true;
    }

    protected virtual bool SetImportList()
    {
        Table table;
        table = new Table();
        table.Compare = new StringCompare();
        table.Compare.Init();
        table.Init();

        this.Module.Import = table;

        Table classTable;
        classTable = this.Module.Class;

        Iter iter;
        iter = classTable.IterCreate();
        classTable.IterSet(iter);
        while (iter.Next())
        {
            ClassClass a;
            a = (ClassClass)iter.Value;

            this.AddClassToImportTable(a.Base);


            Iter iterA;
            iterA = a.Field.IterCreate();
            a.Field.IterSet(iterA);
            while (iterA.Next())
            {
                Field field;
                field = (Field)iterA.Value;
                this.AddClassToImportTable(field.Class);
            }

            iterA = a.Maide.IterCreate();
            a.Maide.IterSet(iterA);
            while (iterA.Next())
            {
                Maide maide;
                maide = (Maide)iterA.Value;
                this.AddClassToImportTable(maide.Class);

                Table varTable;
                varTable = maide.Param;

                Iter iterAa;
                iterAa = varTable.IterCreate();
                varTable.IterSet(iterAa);
                while (iterAa.Next())
                {
                    Var varVar;
                    varVar = (Var)iterAa.Value;

                    this.AddClassToImportTable(varVar.Class);
                }
            }
        }
        return true;
    }

    protected virtual bool AddClassToImportTable(ClassClass varClass)
    {
        if (varClass.Module == this.Module)
        {
            return true;
        }

        Table table;
        table = this.Module.Import;

        string moduleName;
        moduleName = varClass.Module.Ref.Name;
        if (!table.Contain(moduleName))
        {
            Table classTable;
            classTable = new Table();
            classTable.Compare = new StringCompare();
            classTable.Compare.Init();
            classTable.Init();

            this.ListInfra.TableAdd(table, moduleName, classTable);
        }
        Table oo;
        oo = (Table)table.Get(moduleName);

        string name;
        name = varClass.Name;

        if (oo.Contain(name))
        {
            return true;
        }

        ClassClass oa;
        oa = this.ClassGet(moduleName, name);

        this.ListInfra.TableAdd(oo, name, oa);
        return true;
    }

    protected virtual bool ConsoleWrite()
    {
        Iter iter;
        iter = this.ModuleTable.IterCreate();
        this.ModuleTable.IterSet(iter);

        while (iter.Next())
        {
            Module module;
            module = (Module)iter.Value;
            
            global::System.Console.Write("--------------\n");
            global::System.Console.Write(module.Ref.Name + "\n");
            global::System.Console.Write("--------------\n");

            Table table;
            table = module.Class;
            Iter iterA;
            iterA = table.IterCreate();
            table.IterSet(iterA);
            while (iterA.Next())
            {
                ClassClass a;
                a = (ClassClass)iterA.Value;

                global::System.Console.Write("Class: " + a.Name + ", Base: " + a.Base.Name + "(" + a.Base.Module.Ref.Name + ")" + "\n");

                Iter iterB;
                iterB = a.Field.IterCreate();
                a.Field.IterSet(iterB);
                while (iterB.Next())
                {
                    Field field;
                    field = (Field)iterB.Value;
                    global::System.Console.Write("    Field: " + field.Name + ", Count: " + this.CountString(field.Count.Index) + ", Class: " + field.Class.Name + "(" + field.Class.Module.Ref.Name + ")" + "\n");
                }

                iterB = a.Maide.IterCreate();
                a.Maide.IterSet(iterB);
                while (iterB.Next())
                {
                    Maide maide;
                    maide = (Maide)iterB.Value;
                    global::System.Console.Write("    Maide: " + maide.Name + ", Count: " + this.CountString(maide.Count.Index) + ", Class: " + maide.Class.Name + "(" + maide.Class.Module.Ref.Name + ")" + "\n");


                    Table varTable;
                    varTable = maide.Param;
                    Iter iterBa;
                    iterBa = varTable.IterCreate();
                    varTable.IterSet(iterBa);
                    while (iterBa.Next())
                    {
                        Var varVar;
                        varVar = (Var)iterBa.Value;
                        global::System.Console.Write("        Var: " + varVar.Name + ", Class: " + varVar.Class.Name + "(" + varVar.Class.Module.Ref.Name + ")" + "\n");
                    }
                }
            }

            global::System.Console.Write("--------\n");

            iterA = module.Import.IterCreate();
            module.Import.IterSet(iterA);

            while (iterA.Next())
            {
                string oa;
                oa = (string)iterA.Index;

                global::System.Console.Write(oa + "\n");

                Table ob;
                ob = (Table)iterA.Value;

                Iter iterB;
                iterB = ob.IterCreate();
                ob.IterSet(iterB);
                while (iterB.Next())
                {
                    string className;
                    className = (string)iterB.Index;
                    global::System.Console.Write("    " + className + "\n");
                }

                if (!(oa.StartsWith("Avalon.") | oa.StartsWith("Class.")))
                {
                    global::System.Console.Error.Write("Import module is not Avalon Module or Class Compiler Module\n");
                    global::System.Environment.Exit(105);
                }
            }
        }
        return true;
    }

    protected virtual bool ExecuteReferWrite()
    {
        ReferWrite write;
        write = new ReferWrite();
        write.Init();
        write.SystemClass = true;

        Iter iter;
        iter = this.ReferTable.IterCreate();
        this.ReferTable.IterSet(iter);
        while (iter.Next())
        {
            Refer refer;
            refer = (Refer)iter.Value;

            write.Refer = refer;
            write.Execute();

            Data data;
            data = write.Data;
            write.Data = null;

            string name;
            name = refer.Ref.Name;

            string path;
            path = name + ".ref";

            this.StorageInfra.DataWrite(path, data);
        }
        return true;
    }

    protected virtual Module ModuleGet(string module)
    {
        Module a;
        a = (Module)this.ModuleTable.Get(module);
        if (a == null)
        {
            global::System.Console.Error.Write("ModuleGet no module, module: " + module + "\n");
            global::System.Environment.Exit(100);
        }
        return a;
    }

    protected virtual ClassClass ModuleClassGet(Module module, string name)
    {
        ClassClass a;
        a = (ClassClass)module.Class.Get(name);
        if (a == null)
        {
            global::System.Console.Error.Write("ModuleClassGet no class, class: " + name + "(" + module.Ref.Name + ")" + "\n");
            global::System.Environment.Exit(101);
        }
        return a;
    }

    protected virtual ClassClass ClassGetType(SystemType type)
    {
        string module;
        string varClass;
        module = null;
        varClass = null;
        bool b;
        b = this.IsDotNetBuiltInType(type);
        if (b)
        {
            module = "Avalon.Infra";
            BuiltInType oa;
            oa = (BuiltInType)this.DotNetBuiltInTypeTable.Get(type);
            varClass = oa.Name;
        }
        if (!b)
        {
            module = type.Assembly.GetName().Name;
            varClass = type.Name;
        }
        ClassClass a;
        a = this.ClassGet(module, varClass);
        return a;
    }

    protected virtual ClassClass ClassGet(string module, string name)
    {
        Module o;
        o = this.ModuleGet(module);
        ClassClass oa;
        oa = this.ModuleClassGet(o, name);
        return oa;
    }

    protected virtual bool ClassBaseSetAny(string name)
    {
        ClassClass a;
        a = this.ModuleClassGet(this.Module, name);
        a.Base = this.AnyClass;
        return true;
    }

    protected virtual bool ClassPartSetEmpty(string name)
    {
        ClassClass a;
        a = this.ModuleClassGet(this.Module, name);
        a.Field = this.TableCreate();
        a.Maide = this.TableCreate();
        return true;
    }

    protected virtual Table TableCreate()
    {
        Table a;
        a = new Table();
        a.Compare = new StringCompare();
        a.Compare.Init();
        a.Init();
        return a;
    }

    protected virtual bool AddInfraBuiltInClassList()
    {
        this.AddBuiltInClass("Bool");
        this.AddBuiltInClass("Int");
        this.AddBuiltInClass("String");
        return true;
    }

    protected virtual bool AddBuiltInClass(string name)
    {
        int index;
        index = this.Module.Class.Count;

        ClassClass a;
        a = new ClassClass();
        a.Init();
        a.Index = index;
        a.Name = name;
        a.Module = this.Module;

        this.ListInfra.TableAdd(this.Module.Class, a.Name, a);
        return true;
    }

    protected virtual SystemType AnyType(SystemType[] typeArray)
    {
        int count;
        count = typeArray.Length;
        int i;
        i = 0;
        while (i < count)
        {
            SystemType type;
            type = typeArray[i];

            if (type.Name == "Any")
            {
                return type;
            }

            i = i + 1;
        }
        return null;
    }

    protected virtual bool AddDotNetBuiltInType(SystemType type, string name, int systemClass)
    {
        BuiltInType a;
        a = new BuiltInType();
        a.Init();
        a.Type = type;
        a.Name = name;
        a.SystemClass = systemClass;
        this.ListInfra.TableAdd(this.DotNetBuiltInTypeTable, type, a);
        return true;
    }

    protected virtual bool IsDotNetBuiltInType(SystemType type)
    {
        return this.DotNetBuiltInTypeTable.Contain(type);
    }

    protected virtual Count CountGet(MethodInfo method)
    {
        Count a;
        a = null;
        if (method.IsPublic)
        {
            a = this.CountList.Prudate;
        }
        if (method.IsAssembly)
        {
            a = this.CountList.Probate;
        }
        if (method.IsFamily)
        {
            a = this.CountList.Precate;
        }
        if (method.IsPrivate)
        {
            a = this.CountList.Private;
        }
        return a;
    }

    protected virtual string CountString(int value)
    {
        return (string)this.CountArray.Get(value);
    }

    protected virtual bool SetCountString(string o)
    {
        int index;
        index = this.Index;
        this.CountArray.Set(index, o);
        index = index + 1;
        this.Index = index;
        return true;
    }

    protected virtual bool IsInAbstract(MethodInfo method)
    {
        return method.IsPublic | method.IsFamily;
    }
}