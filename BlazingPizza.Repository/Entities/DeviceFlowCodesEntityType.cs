﻿// <auto-generated />
using System;
using System.Reflection;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace BlazingPizza.Repository.Entities
{
    internal partial class DeviceFlowCodesEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes",
                typeof(DeviceFlowCodes),
                baseEntityType);

            var userCode = runtimeEntityType.AddProperty(
                "UserCode",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("UserCode", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<UserCode>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                maxLength: 200);

            var clientId = runtimeEntityType.AddProperty(
                "ClientId",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("ClientId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<ClientId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 200);

            var creationTime = runtimeEntityType.AddProperty(
                "CreationTime",
                typeof(DateTime),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("CreationTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<CreationTime>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var data = runtimeEntityType.AddProperty(
                "Data",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("Data", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<Data>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 50000);

            var description = runtimeEntityType.AddProperty(
                "Description",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("Description", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<Description>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 200);

            var deviceCode = runtimeEntityType.AddProperty(
                "DeviceCode",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("DeviceCode", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<DeviceCode>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 200);

            var expiration = runtimeEntityType.AddProperty(
                "Expiration",
                typeof(DateTime?),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("Expiration", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<Expiration>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var sessionId = runtimeEntityType.AddProperty(
                "SessionId",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("SessionId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<SessionId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 100);

            var subjectId = runtimeEntityType.AddProperty(
                "SubjectId",
                typeof(string),
                propertyInfo: typeof(DeviceFlowCodes).GetProperty("SubjectId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(DeviceFlowCodes).GetField("<SubjectId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 200);

            var key = runtimeEntityType.AddKey(
                new[] { userCode });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { deviceCode },
                unique: true);

            var index0 = runtimeEntityType.AddIndex(
                new[] { expiration });

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "DeviceCodes");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
