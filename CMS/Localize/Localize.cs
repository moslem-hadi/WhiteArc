using System;
using System.Web;
using System.Resources;
using Westwind.Globalization;

namespace CMS.Localize
{
    public class GeneratedResourceSettings
    {
        // You can change the ResourceAccess Mode globally in Application_Start        
        public static ResourceAccessMode ResourceAccessMode = ResourceAccessMode.AspNetResourceProvider;
    }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "2.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppInfo
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("CMS.Localize.AppInfo", typeof(AppInfo).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String WebsiteName
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("AppInfo","WebsiteName",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}

  [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "2.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CommonWords
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("CMS.Localize.CommonWords", typeof(CommonWords).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String Hello
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("CommonWords","Hello",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}

  [System.CodeDom.Compiler.GeneratedCodeAttribute("Westwind.Globalization.StronglyTypedResources", "2.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LinkText
    {
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    var temp = new ResourceManager("CMS.Localize.LinkText", typeof(LinkText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        private static ResourceManager resourceMan = null;

		public static System.String Home
		{
			get
			{
				return GeneratedResourceHelper.GetResourceString("Words","Home",ResourceManager,GeneratedResourceSettings.ResourceAccessMode);
			}
		}

	}

}
