﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Sc.DataMappers;
using NUnit.Framework;

namespace Glass.Mapper.Sc.Integration.DataMappers
{
    [TestFixture]
    public class SitecoreFieldGuidMapperFixture : AbstractMapperFixture 
    {
        #region Method - GetFieldValue

        [Test]
        public void GetFieldValue_ContainsGuid_GuidObjectReturned()
        {
            //Assign
            var fieldValue = "{FC1D0AFD-71CC-47e2-84B3-7F1A2973248B}";
            var item = Database.GetItem("/sitecore/content/Tests/DataMappers/SitecoreFieldGuidMapper/GetFieldValue");
            var field = item.Fields[FieldName];
            var expected = new Guid(fieldValue);

            var mapper = new SitecoreFieldGuidMapper();

            using (new ItemEditing(item, true))
            {
                field.Value = fieldValue;
            }

            //Act
            var result = (Guid)mapper.GetFieldValue(field, null, null);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetFieldValue_FieldEmpty_EmptyGuidReturned()
        {
            //Assign
            var fieldValue = string.Empty;
            var item = Database.GetItem("/sitecore/content/Tests/DataMappers/SitecoreFieldGuidMapper/GetFieldValue");
            var field = item.Fields[FieldName];
            var expected = Guid.Empty;

            var mapper = new SitecoreFieldGuidMapper();

            using (new ItemEditing(item, true))
            {
                field.Value = fieldValue;
            }

            //Act
            var result = (Guid)mapper.GetFieldValue(field, null, null);

            //Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Method - SetFieldValue

        [Test]
        public void SetFieldValue_Guidpassed_ValueSetOnField()
        {
            //Assign
            var expected = "{FC1D0AFD-71CC-47E2-84B3-7F1A2973248B}";
            var item = Database.GetItem("/sitecore/content/Tests/DataMappers/SitecoreFieldGuidMapper/SetFieldValue");
            var field = item.Fields[FieldName];
            var value = new Guid(expected);

            var mapper = new SitecoreFieldGuidMapper();

            using (new ItemEditing(item, true))
            {
                field.Value = string.Empty;
            }

            //Act
            using (new ItemEditing(item, true))
            {
                mapper.SetFieldValue(field, value, null, null);
            }
            //Assert
            Assert.AreEqual(expected, field.Value);
        }

        [Test]
        public void SetFieldValue_EmptyGuidPassed_ValueSetOnField()
        {
            //Assign
            var expected = "{00000000-0000-0000-0000-000000000000}";
            var item = Database.GetItem("/sitecore/content/Tests/DataMappers/SitecoreFieldGuidMapper/SetFieldValue");
            var field = item.Fields[FieldName];
            var value = new Guid(expected);

            var mapper = new SitecoreFieldGuidMapper();

            using (new ItemEditing(item, true))
            {
                field.Value = string.Empty;
            }

            //Act
            using (new ItemEditing(item, true))
            {
                mapper.SetFieldValue(field, value, null, null);
            }
            //Assert
            Assert.AreEqual(expected, field.Value);
        }

        [Test]
        [ExpectedException(typeof(MapperException))]
        public void SetFieldValue_IntegerPassed_ValueSetOnField()
        {
            //Assign
            var item = Database.GetItem("/sitecore/content/Tests/DataMappers/SitecoreFieldGuidMapper/SetFieldValue");
            var field = item.Fields[FieldName];
            var value = 1;

            var mapper = new SitecoreFieldGuidMapper();

            using (new ItemEditing(item, true))
            {
                field.Value = string.Empty;
            }

            //Act
            using (new ItemEditing(item, true))
            {
                mapper.SetFieldValue(field, value, null, null);
            }
            //Assert
        }

        #endregion
    }

   
}