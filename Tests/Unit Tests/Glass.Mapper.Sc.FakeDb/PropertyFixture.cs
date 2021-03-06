﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Fluent;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.FakeDb;

namespace Glass.Mapper.Sc.FakeDb
{
    /// <summary>
    /// Test mapping of different property accessors.
    /// </summary>
    [TestFixture]
    public class PropertyFixture
    {

        [Test]
        public void Private_MapsDataToPrivateField()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string expected = "Field Value";
            using (Db database = new Db
            {
                new Sitecore.FakeDb.DbItem("Target", new ID(id))
                {
                    {"Field1",expected }
                }
            })
            {

                var context = Context.Create(Utilities.CreateStandardResolver());
                var loader = new SitecoreFluentConfigurationLoader();
                 loader.Add(PrivateModel.Load());
                context.Load(loader);

                var service = new SitecoreService(database.Database, context);

                //Act
                var result = service.GetItem<PrivateModel>(id, x => { });

                //Arrange
                Assert.AreEqual(expected, result.Field1Public);


            }

        }

        [Test]
        public void PropertyWithField_FieldNotPopulated()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string expected = "Field Value";
            using (Db database = new Db
            {
                new Sitecore.FakeDb.DbItem("Target", new ID(id))
                {
                    {"Field1",expected }
                }
            })
            {

                var context = Context.Create(Utilities.CreateStandardResolver());
              
                var service = new SitecoreService(database.Database, context);

                //Act
                var result = service.GetItem<ModelWithField>(id, x => { });

                //Arrange
                Assert.AreEqual(expected, result.Field1);
                Assert.IsNull(result.GetField());
            }

        }


        [Test]
        public void Private_Recursive_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string expected = "Field Value";
            using (Db database = new Db
            {
                new Sitecore.FakeDb.DbItem("Target", new ID(id))
                {
                    {"Field1",id.ToString() }
                }
            })
            {

                var context = Context.Create(Utilities.CreateStandardResolver());
                var loader = new SitecoreFluentConfigurationLoader();
                loader.Add(PrivateModelRecursive.Load());
                context.Load(loader);

                var service = new SitecoreService(database.Database, context);

                //Act
                //Arrange
                var exception = Assert.Throws(typeof(MapperException), () =>
                {
                    var result = service.GetItem<PrivateModelRecursive>(id, x => { });
                });

                while ((exception is MapperStackException) == false && exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                Assert.IsTrue(exception is MapperStackException);
            }
        }


        [Test]
        public void Private_RecursiveIndirect_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string expected = "Field Value";
            using (Db database = new Db
            {
                new Sitecore.FakeDb.DbItem("Target", new ID(id))
                {
                    {"Field1",id.ToString() }
                }
            })
            {

                var context = Context.Create(Utilities.CreateStandardResolver());
                var loader = new SitecoreFluentConfigurationLoader();
                loader.Add(PrivateModelRecursive.Load());
                loader.Add(PrivateModelIndirectRecursive.Load());
                context.Load(loader);



                var service = new SitecoreService(database.Database, context);

                //Act
                //Arrange
                var exception = Assert.Throws(typeof(MapperException), () =>
                {
                    var result = service.GetItem<PrivateModelIndirectRecursive>(id, x => { });
                });

                while ((exception is MapperStackException) == false && exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                Assert.IsTrue(exception is MapperStackException);
            }

        }

        public class PrivateModel
        {
            private string Field1 { get; set; }

            public static SitecoreType<PrivateModel> Load()
            {
                var config = new SitecoreType<PrivateModel>();
                config.Field(x => x.Field1);
                return config;
            }

            public string Field1Public
            {
                get { return Field1; }
            }
        }


        public class PrivateModelIndirectRecursive
        {
            private PrivateModelRecursive Field1 { get; set; }

            public static SitecoreType<PrivateModelIndirectRecursive> Load()
            {
                var config = new SitecoreType<PrivateModelIndirectRecursive>();
                config.Field(x => x.Field1);
                return config;
            }

            public PrivateModelRecursive Field1Public
            {
                get { return Field1; }
            }
        }

        public class PrivateModelRecursive
        {
            private PrivateModelRecursive Field1 { get; set; }

            public static SitecoreType<PrivateModelRecursive> Load()
            {
                var config = new SitecoreType<PrivateModelRecursive>();
                config.Field(x => x.Field1);
                return config;
            }

            public PrivateModelRecursive Field1Public
            {
                get { return Field1; }
            }
        }

        public class ModelWithField
        {
            private string _field1;
            public virtual string Field1
            {
                get { return _field1; }
                set { _field1 = value; }
            }

            public string GetField()
            {
                return _field1;
            }
        }
    }
}
