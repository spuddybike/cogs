﻿using Cogs.Dto;
using Cogs.Model;
using Cogs.Publishers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Cogs.Tests
{
    public class JsonSchemaTests
    {
        [Fact]
        public async System.Threading.Tasks.Task asyncJsonSchemaTestAsync()
        {
            string path = "..\\..\\..\\..\\cogsburger";

            string subdir = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string outputPath = Path.Combine(Path.GetTempPath(), subdir);

            var directoryReader = new CogsDirectoryReader();
            var cogsDtoModel = directoryReader.Load(path);
            
            var modelBuilder = new CogsModelBuilder();
            var cogsModel = modelBuilder.Build(cogsDtoModel);

            var jsonPublisher = new JsonPublisher();
            jsonPublisher.TargetDirectory = outputPath;
            jsonPublisher.Publish(cogsModel);

            var schemadata = File.ReadAllText(Path.Combine(outputPath, "jsonSchema" + ".json"));
            var schema = await JsonSchema4.FromJsonAsync(schemadata);
            var jsondata = File.ReadAllText(@"C: \Users\clement\Desktop\JsonFolder\testing1_reference_reusable.json");
            var validate = schema.Validate(jsondata);

            Assert.Null(validate);
            //var schema = File.ReadAllText(Path.Combine(outputPath, "jsonSchema" + ".json"));
            //var valida = schema.validate();

            //var valid1 = schema.Validate();
            //read JSON directly from a file
            //Assert.True(valid1);
            //Assert.True(valid2);
        }
    }
}