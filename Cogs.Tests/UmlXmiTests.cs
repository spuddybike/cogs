﻿using System;
using System.Collections.Generic;
using System.Text;
using Cogs.Dto;
using Cogs.Model;
using Cogs.Publishers;
using System.IO;
using Xunit;

namespace Cogs.Tests
{
    public class UmlXmiTests
    {
        [Fact]
        public void UmlForHamburgersTest()
        {
            string path = "..\\..\\..\\..\\cogsburger";

            string subdir = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string outputPath = Path.Combine(Path.GetTempPath(), subdir);

            var directoryReader = new CogsDirectoryReader();
            var cogsDtoModel = directoryReader.Load(path);

            var modelBuilder = new CogsModelBuilder();
            var cogsModel = modelBuilder.Build(cogsDtoModel);

            // test both normative and not normative outputs
            var publisher = new UmlSchemaPublisher();
            publisher.TargetDirectory = outputPath;
            publisher.Normative = false;
            publisher.DotLocation = "C:\\Users\\kevin\\Downloads\\graphviz-2.38\\release\\bin";
            publisher.Publish(cogsModel);

            publisher = new UmlSchemaPublisher();
            publisher.TargetDirectory = outputPath;
            publisher.Normative = true;
            publisher.Publish(cogsModel);


            // TODO use xml importer to check that xml is properly formed.
            // For now we are just making sure there are no errors while running.


        }
    }
}
