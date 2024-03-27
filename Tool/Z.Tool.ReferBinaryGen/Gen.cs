namespace Z.Tool.ReferBinaryGen;

public class Gen : Any
{
    public override bool Init()
    {
        base.Init();
        this.ListInfra = ListInfra.This;

        ListList list;
        list = new ListList();
        list.Init();
        list.Add(typeof(bool));
        list.Add(typeof(int));
        list.Add(typeof(uint));
        list.Add(typeof(long));
        list.Add(typeof(ulong));
        list.Add(typeof(short));
        list.Add(typeof(ushort));
        list.Add(typeof(byte));
        list.Add(typeof(sbyte));
        list.Add(typeof(char));
        list.Add(typeof(string));

        this.DotNetBuiltInTypeArray = this.ListInfra.ArrayCreateList(list);

        return true;
    }

    protected virtual ListInfra ListInfra { get; set; }

    protected virtual Assembly Assembly { get; set; }
    protected virtual Array DotNetTypeArray { get; set; }
    protected virtual Table Import { get; set; }
    protected virtual Array DotNetBuiltInTypeArray { get; set; }

    public virtual int Execute()
    {
        this.Assembly = typeof(Any).Assembly;
        this.ExecuteAssembly();

        this.Assembly = typeof(ListList).Assembly;
        this.ExecuteAssembly();

        return 0;
    }

    protected virtual bool ExecuteAssembly()
    {
        this.DotNetTypeList();

        this.ImportTable();

        this.ConsoleWrite();

        return true;
    }

    protected virtual bool ImportTable()
    {
        Table table;
        table = new Table();
        table.Compare = new StringCompare();
        table.Compare.Init();
        table.Init();

        this.Import = table;

        Array array;
        array = this.DotNetTypeArray;

        int count;
        count = array.Count;
        int i;
        i = 0;
        while (i < count)
        {
            DotNetType a;
            a = (DotNetType)array.Get(i);
            
            SystemType type;
            type = a.Type;

            SystemType baseType;
            baseType = type.BaseType;

            this.AddTypeToImportTable(baseType);

            int countA;
            int iA;

            countA = a.Property.Count;
            iA = 0;
            while (iA < countA)
            {
                PropertyInfo property;
                property = (PropertyInfo)a.Property.Get(iA);
                this.AddTypeToImportTable(property.PropertyType);
                iA = iA + 1;
            }

            countA = a.Method.Count;
            iA = 0;
            while (iA < countA)
            {
                MethodInfo method;
                method = (MethodInfo)a.Method.Get(iA);
                this.AddTypeToImportTable(method.ReturnType);
                iA = iA + 1;
            }

            i = i + 1;
        }
        return true;
    }

    protected virtual bool AddTypeToImportTable(SystemType type)
    {
        Assembly assembly;
        assembly = null;

        bool b;
        b = this.DotNetBuiltInTypeArray.Contain(type);
        if (b)
        {
            assembly = typeof(Any).Assembly;
        }
        if (!b)
        {
            assembly = type.Assembly;
        }

        if (assembly == this.Assembly)
        {
            return true;
        }

        Table table;
        table = this.Import;
        
        string assemblyName;
        assemblyName = assembly.GetName().Name;
        if (!table.Contain(assemblyName))
        {
            Table typeTable;
            typeTable = new Table();
            typeTable.Compare = new StringCompare();
            typeTable.Compare.Init();
            typeTable.Init();

            ListEntry oa;
            oa = new ListEntry();
            oa.Init();
            oa.Index = assemblyName;
            oa.Value = typeTable;
            table.Add(oa);
        }
        Table oo;
        oo = (Table)table.Get(assemblyName);

        string name;
        name = type.Name;

        if (oo.Contain(name))
        {
            return true;
        }
        
        ListEntry ob;
        ob = new ListEntry();
        ob.Init();
        ob.Index = name;
        ob.Value = type;
        oo.Add(ob);
        return true;
    }

    protected virtual bool DotNetTypeList()
    {
        Assembly o;
        o = this.Assembly;

        ListList list;
        list = new ListList();
        list.Init();

        SystemType[] typeArray;
        typeArray = o.GetExportedTypes();

        int count;
        count = typeArray.Length;
        int i;
        i = 0;
        while (i < count)
        {
            DotNetType a;
            a = new DotNetType();
            a.Init();

            SystemType type;
            type = typeArray[i];

            SystemType baseType;
            baseType = type.BaseType;

            a.Type = type;

            PropertyInfo[] propertyArrayA;
            propertyArrayA = type.GetProperties(BindingFlag.Instance | BindingFlag.Public | BindingFlag.NonPublic | BindingFlag.DeclaredOnly | BindingFlag.ExactBinding);

            MethodInfo[] methodArrayA;
            methodArrayA = type.GetMethods(BindingFlag.Instance | BindingFlag.Public | BindingFlag.NonPublic | BindingFlag.DeclaredOnly | BindingFlag.ExactBinding);

            int countA;
            int iA;
            ListList propertyList;
            propertyList = new ListList();
            propertyList.Init();

            countA = propertyArrayA.Length;
            iA = 0;
            while (iA < countA)
            {
                PropertyInfo property;
                property = propertyArrayA[iA];

                if (!property.IsSpecialName & property.CanRead & property.CanWrite)
                {
                    if (this.IsInAbstract(property.GetMethod) & !((type == typeof(Data)) & (property.Name == "Value")))
                    {
                        propertyList.Add(property);
                    }
                }

                iA = iA + 1;
            }

            Array propertyArray;
            propertyArray = this.ListInfra.ArrayCreateList(propertyList);

            ListList methodList;
            methodList = new ListList();
            methodList.Init();

            countA = methodArrayA.Length;
            iA = 0;
            while (iA < countA)
            {
                MethodInfo method;
                method = methodArrayA[iA];

                if (!method.IsSpecialName & this.IsInAbstract(method))
                {
                    methodList.Add(method);
                }

                iA = iA + 1;
            }

            Array methodArray;
            methodArray = this.ListInfra.ArrayCreateList(methodList);

            a.Property = propertyArray;
            a.Method = methodArray;

            list.Add(a);

            i = i + 1;
        }

        Array array;
        array = this.ListInfra.ArrayCreateList(list);

        this.DotNetTypeArray = array;
        return true;
    }

    protected virtual bool ConsoleWrite()
    {
        global::System.Console.Write("--------------\n");
        global::System.Console.Write(this.Assembly.GetName().Name + "\n");
        global::System.Console.Write("--------------\n");

        int count;
        count = this.DotNetTypeArray.Count;
        int i;
        i = 0;
        while (i < count)
        {
            DotNetType a;
            a = (DotNetType)this.DotNetTypeArray.Get(i);

            SystemType type;
            type = a.Type;

            SystemType baseType;
            baseType = type.BaseType;

            global::System.Console.Write("Class: " + type.Name + ", Base: " + baseType.Name + "(" + baseType.Assembly.GetName().Name + ")" + "\n");

            int countA;
            int iA;

            countA = a.Property.Count;
            iA = 0;
            while (iA < countA)
            {
                PropertyInfo property;
                property = (PropertyInfo)a.Property.Get(iA);
                global::System.Console.Write("Field: " + property.Name + ", Count: " + this.CountString(property.GetMethod) + ", ResultType: " + property.PropertyType.Name + "\n");
                iA = iA + 1;
            }

            countA = a.Method.Count;
            iA = 0;
            while (iA < countA)
            {
                MethodInfo method;
                method = (MethodInfo)a.Method.Get(iA);
                global::System.Console.Write("Maide: " + method.Name + ", Count: " + this.CountString(method) + ", ResultType: " + method.ReturnType.Name + "\n");
                iA = iA + 1;
            }

            i = i + 1;
        }

        global::System.Console.Write("--------\n");

        Iter iter;
        iter = this.Import.IterCreate();
        this.Import.IterSet(iter);

        while (iter.Next())
        {
            string assemblyName;
            assemblyName = (string)iter.Index;

            global::System.Console.Write(assemblyName + "\n");

            Table table;
            table = (Table)iter.Value;

            Iter iterA;
            iterA = table.IterCreate();
            table.IterSet(iterA);
            while (iterA.Next())
            {
                string typeName;
                typeName = (string)iterA.Index;
                global::System.Console.Write("    " + typeName + "\n");
            }
        }
        return true;
    }

    protected virtual string CountString(MethodInfo method)
    {
        if (method.IsPublic)
        {
            return "Prudate";
        }
        if (method.IsAssembly)
        {
            return "Probate";
        }
        if (method.IsFamily)
        {
            return "Precate";
        }
        if (method.IsPrivate)
        {
            return "Private";
        }
        return null;
    }

    protected virtual bool IsInAbstract(MethodInfo method)
    {
        return method.IsPublic | method.IsFamily;
    }
}