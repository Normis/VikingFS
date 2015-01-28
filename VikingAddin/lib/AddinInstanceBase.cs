using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;
using TaxprepAddinAPI;

namespace TxpAddinLibrary
{        

    public abstract class AddinInstanceBase : IAddinInstance
    {
        protected IAppInstance _appInstance = null;

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class AddinInstance : Attribute
        {
            public AddinInstance() 
            {
            }
        }

        private static AddinInstanceBase FactoryInitializer()
        {
            // force static constructors in types specified by InitializeOnLoad
            var classes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(AddinInstance), false).Count() != 0)
                .OrderBy(t => t.Name);

            foreach (var LType in classes)
                return (AddinInstanceBase) Assembly.GetExecutingAssembly().CreateInstance(LType.FullName, false, 
                    BindingFlags.Default, null, null, System.Globalization.CultureInfo.CurrentCulture, null);

            throw new Exception("Could not locate the addin instance");
        }

        //[STAThread]
        [DllExport("GetAddinInstance", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static int GetAddinInstance(out IAddinInstance addinInstance)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += DoAssemblyResolve;

                addinInstance = FactoryInitializer();

                return 0;
            }
            catch (Exception error)
            {
                addinInstance = null;
                return Marshal.GetHRForException(error);
            }
        }

        //[STAThread]
        [DllExport("ReleaseAddinInstance", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static int ReleaseAddinInstance()
        {
            try
            {
                // !!! make sure all interfaces are released by garbage collector
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return 0;
            }
            catch (Exception error)
            {
                return Marshal.GetHRForException(error);
            }
        }

        #region IAddinInstance Implementation
        //['{FBE92BC9-B889-49A3-A70D-FE4129071301}']
        public Guid Key { get; private set; }
        public string Name { get; private set; }
        public string Version { get; private set; }
        public void Initialize([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TxpAddinLibrary.AddinMarshaler))] IAppInstance appInstance)
        {
            _appInstance = appInstance;
            InitializeTaxPrepAddin();
        }

        #endregion

        public virtual void ReleaseApp()
        {
            // do nothing
        }

        private static Assembly DoAssemblyResolve(Object sender, ResolveEventArgs e)
        {
            //Debug.WriteLine(string.Format("Resolving Assembly \"{0}\"", e.Name));
            var lSearchPath = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            if (lSearchPath == null)
                return null;

            var lFullFileName = System.IO.Path.Combine(System.IO.Path.GetFullPath(lSearchPath), new AssemblyName(e.Name).Name) + ".dll";
            if (!System.IO.File.Exists(lFullFileName))
                return null;

            var lAssemblyFullName = AssemblyName.GetAssemblyName(lFullFileName).FullName;

            if (String.Compare(lAssemblyFullName, e.Name, StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            //Debug.WriteLine(string.Format("Resolved Assembly \"{0}\" as \"{1}\"", e.Name, lFullFileName));
            return Assembly.LoadFrom(lFullFileName);
        }

        public AddinInstanceBase(Guid AKey, string AName, string AVersion)
        {
            Key = AKey;
            Name = AName;
            Version = AVersion;
        }

        public abstract void InitializeTaxPrepAddin();

    }
}
