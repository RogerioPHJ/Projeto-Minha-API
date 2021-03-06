﻿using FS.MinhaApi.Api.HATEOAS.ResourceBuilders.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace FS.MinhaApi.Api.HATEOAS.Helpers
{
    public class RestResourceBuilder
    {
        public static void BuildResource(object resource, HttpRequestMessage request)
        {
            IEnumerable enumerable = resource as IEnumerable;
            Type dtoType;   
            if (enumerable == null)
            {
                dtoType = resource.GetType();
            }
            else
            {
                dtoType = resource.GetType().GetGenericArguments()[0];
            }
            if ((dtoType.BaseType != typeof(RestResource)))
            {
                throw new ArgumentException($"Era esperado um RestResource, porem, foi informado um {resource.GetType().FullName}");
            }
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            IResourceBuilder resourceBuilder
                = (IResourceBuilder)Activator.CreateInstance(currentAssembly.GetType($"FS.MinhaApi.Api.HATEOAS.ResourceBuilders.Impl.{dtoType.Name}ResourceBuilder"));
            if (enumerable == null)
            {
                resourceBuilder.BuildResource(resource, request);
            }
            else
            {
                foreach (var item in enumerable)
                {
                    resourceBuilder.BuildResource(item, request);
                }
            }
        }
    }
}