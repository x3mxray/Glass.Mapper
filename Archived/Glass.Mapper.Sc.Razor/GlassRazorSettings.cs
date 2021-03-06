/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//-CRE-

namespace Glass.Mapper.Sc.Razor
{
    /// <summary>
    /// Class GlassRazorModuleLoader
    /// </summary>
    public class GlassRazorSettings
    {
         static GlassRazorSettings()
         {
             CatchExceptions = true;
         }
        /// <summary>
        /// The context name
        /// </summary>
        public const string ContextName = "GlassRazor";

        public static bool CatchExceptions { get; set; }

        ///// <summary>
        ///// Loads the specified resolver.
        ///// </summary>
        ///// <param name="resolver">The resolver.</param>
        ///// <returns>Context.</returns>
        //public static Context Load(IDependencyResolver resolver)
        //{
        //    var context = Context.Contexts.ContainsKey(ContextName) ? Context.Contexts[ContextName] : Context.Create(resolver, ContextName);

        //    var loader = new SitecoreAttributeConfigurationLoader("Glass.Mapper.Sc.Razor");
        //    context.Load(loader);

        //    return context;
        //}

    }
}

