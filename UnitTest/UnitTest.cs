using Algorithms;
using System.Collections;
using static Algorithms.HuffmanAlgorithm;

namespace AlgorithmTests
{

    public class KruskalAlgorithmTest : KruskalAlgorithm
    {
        [Fact]
        public void WorstCase()
        {
            // Arrange
            var numberOfVertices = 5;
            var edges = new List<Edge>();

            // Actt
            var result = Kruskal(numberOfVertices, edges);

            // Assert
            Assert.Empty(result);
        }



        [Fact]
        public void AverageCase()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 5),
                new Edge(0, 2, 3),
                new Edge(0, 3, 2),
                new Edge(1, 2, 4),
                new Edge(1, 3, 3),
                new Edge(2, 3, 1),
                new Edge(2, 4, 4),
                new Edge(3, 4, 6),
            };

            var result = Kruskal(5, edges);

            Assert.Equal(4, result.Count);
            Assert.Contains(new Edge(0, 3, 2), result);
            Assert.Contains(new Edge(2, 3, 1), result);
            Assert.Contains(new Edge(1, 3, 3), result);
            Assert.Contains(new Edge(2, 4, 4), result);


        }





        [Fact]
        public void BestCase()
        {
            var trees = new List<List<Edge>>();
            var rand = new Random();
            for (int i = 0; i < 70; i++)
            {
                var edges = new List<Edge>();
                for (int j = 0; j < 4; j++)
                {
                    for (int k = j + 1; k < 5; k++)
                    {
                        edges.Add(new Edge(j, k, rand.Next(1, 10)));
                    }
                }
                trees.Add(Kruskal(5, edges));
            }


            foreach (var tree in trees)
            {
                Assert.Equal(4, tree.Count);
            }


            for (int i = 0; i < 70; i++)
            {
                for (int j = i + 1; j < 70; j++)
                {
                    Assert.False(AreEqual(trees[i], trees[j]));
                }
            }
        }

        private bool AreEqual(List<Edge> tree1, List<Edge> tree2)
        {
            if (tree1.Count != tree2.Count)
            {
                return false;
            }
            foreach (var edge in tree1)
            {
                if (!tree2.Contains(edge))
                {
                    return false;
                }
            }
            return true;
        }
    }




    public class BelmannFordAlgorithmTest : BellmanFordAlgorithm
    {
        [Fact]
        public void WorstCase()
        {
            var edges = new List<Edge>
        {
            new Edge { Start = 0, End = 1, Weight = 4 },
            new Edge { Start = 0, End = 2, Weight = 8 },
            new Edge { Start = 1, End = 3, Weight = 5 },
            new Edge { Start = 1, End = 2, Weight = 2 },
            new Edge { Start = 2, End = 3, Weight = 5 },
            new Edge { Start = 2, End = 4, Weight = 9 },
            new Edge { Start = 3, End = 4, Weight = 4 },
            new Edge { Start = 4, End = 5, Weight = 3 }
        };

            var shortestPath = ShortestPath(edges, 0, 5);

            Assert.Equal(new List<int> { 0, 1, 3, 4, 5 }, shortestPath);
        }





        [Fact]
        public void AverageCase()
        {
            // Arrange
            var edges = new List<Edge>
        {
            new Edge { Start = 1, End = 2, Weight = 3 },
            new Edge { Start = 1, End = 3, Weight = 2 },
            new Edge { Start = 1, End = 4, Weight = 7 },
            new Edge { Start = 2, End = 4, Weight = 2 },
            new Edge { Start = 2, End = 5, Weight = 1 },
            new Edge { Start = 3, End = 4, Weight = 1 },
            new Edge { Start = 3, End = 5, Weight = 4 },
            new Edge { Start = 4, End = 5, Weight = 3 },
            new Edge { Start = 4, End = 6, Weight = 2 },
            new Edge { Start = 5, End = 6, Weight = 1 }
        };

            // Act
            var shortestPath = ShortestPath(edges, 1, 6);

            // Assert
            var expectedPath = new List<int> { 1, 3, 4, 6 };
            Assert.Equal(expectedPath, shortestPath);
        }





        [Fact]
        public void BestCase()
        {

            var edges = new List<Edge>
        {
            new Edge { Start = 0, End = 1, Weight = 5 },
            new Edge { Start = 0, End = 2, Weight = 4 },
            new Edge { Start = 1, End = 3, Weight = 3 },
            new Edge { Start = 2, End = 1, Weight = -6 },
            new Edge { Start = 3, End = 4, Weight = 2 },
            new Edge { Start = 4, End = 2, Weight = -10 },
            new Edge { Start = 4, End = 5, Weight = 1 },
            new Edge { Start = 5, End = 3, Weight = -2 },
            new Edge { Start = 5, End = 6, Weight = 4 },
            new Edge { Start = 6, End = 4, Weight = 1 }
        };


            int startNode = 0;
            int endNode = 4;


            Assert.Throws<ArgumentException>(() => ShortestPath(edges, startNode, endNode));
        }

    }




    [Collection("my collection")]
    public class HuffmanCoding
    {

        [Fact]
        public void TextFileAndSoundFileSizeTest()
        {

            string testFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "/Test";

            string soundPath = testFilePath + "/Test.mp3";
            string textPath = testFilePath + "/";
            string soundName = soundPath.Split("/").Last();
            soundName = soundName.Split(".").First();



            Thread t = new Thread(Mp3_CompressandDecompress);
            FileInfo FileInfo = new FileInfo(soundPath);

            int size = ((int)FileInfo.Length);


            string loremIpsumText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non risus. Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, ultricies sed, dolor. Cras elementum ultrices diam. Maecenas ligula massa, varius a, semper congue, euismod non, mi.";
            t.Start();
            int a = System.Text.ASCIIEncoding.ASCII.GetByteCount(loremIpsumText);
            string txt_bytes = "";
            string remaining = "a";

            int repeat = size / a;
            int over = size % a;

            for (int i = 0; i < repeat; i++)
            {
                txt_bytes += loremIpsumText;
            }
            for (int i = 0; i < over; i++)
            {
                txt_bytes += remaining;
            }
            string textTestPath = textPath + soundName + ".txt";
            File.WriteAllText(textTestPath, txt_bytes);


            string textFile = File.ReadAllText(textTestPath);
            int TestTextSize = textFile.Length;
            Assert.True(TestTextSize == size, "Sound File and Text Files size are Equal");



            HuffmanTree text = new HuffmanTree();

            text.Build(textFile);

            BitArray compressedText = text.Encode(textFile);

            using (FileStream compressedTextFile = new FileStream(textPath + "CompressedText", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(compressedTextFile))
                {
                    WriteBitArray(writer, compressedText);
                }
            }





            using (FileStream decompressedTextFile = new FileStream(textPath + "CompressedText", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(decompressedTextFile))
                {
                    BitArray encodedFromFile = ReadBitArray(reader, decompressedTextFile.Length);
                    string decoded = text.Decode(encodedFromFile);
                    File.WriteAllText(textPath + "DecompressedText.txt", decoded);
                }
            }
            t.Join();
            void Mp3_CompressandDecompress()
            {
                byte[] originalData = File.ReadAllBytes(testFilePath + "/Test.mp3");

                string inputFilePath = testFilePath + "/Test.mp3";
                string compressedFilePath = testFilePath + "/CompressedMp3";
                string decompressedFilePath = testFilePath + "/Decompressed.mp3";


                byte[] input = File.ReadAllBytes(inputFilePath);


                HuffmanTree_mp3 mp3 = new HuffmanTree_mp3();
                mp3.Build(input);
                BitArray encoded = mp3.Encode(input);


                using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(compressedFileStream))
                    {
                        WriteBitArray(writer, encoded);
                    }
                }


                using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(compressedFileStream))
                    {
                        BitArray encodedFromFile = ReadBitArray(reader, compressedFileStream.Length);


                        byte[] decodedBytes = mp3.Decode(encodedFromFile);


                        File.WriteAllBytes(decompressedFilePath, decodedBytes);
                    }
                }




            }



        }

    }

    [Collection("my collection")]
    public class HuffmanCodingTest
    {

        [Fact]
        public void TextFileCompressTest()
        {
            string testFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "/Test";
            string compressedTextPath = testFilePath + "/CompressedText";
            string TextPath = testFilePath + "/Test.txt";
            long compressedTextSize = new System.IO.FileInfo(compressedTextPath).Length;
            long TextSize = new System.IO.FileInfo(TextPath).Length;

            Assert.True(compressedTextSize < TextSize);

        }

        [Fact]
        public void Mp3FileCompressTest()
        {
            string testFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "/Test";

            string mp3Path = testFilePath + "/Test.mp3";
            string compressedMp3Path = testFilePath + "/CompressedMp3";
            long mp3Size = new System.IO.FileInfo(mp3Path).Length;
            long compressedMp3Size = new System.IO.FileInfo(compressedMp3Path).Length;

            Assert.True(compressedMp3Size < mp3Size);


        }


        [Fact]
        public void TextFileDecompressTest()
        {
            string testFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "/Test";

            string TextPath = testFilePath + "/Test.txt";
            string decompressedTextPath = testFilePath + "/DecompressedText.txt";
            long TextSize = new System.IO.FileInfo(TextPath).Length;
            long decompressedTextSize = new System.IO.FileInfo(decompressedTextPath).Length;

            Assert.True(decompressedTextSize == TextSize || TextSize - decompressedTextSize <= 5);


        }

        [Fact]
        public void Mp3FileDecompressTest()
        {
            string testFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "/Test";

            string mp3Path = testFilePath + "/Test.mp3";
            string decompressedMp3Path = testFilePath + "/Decompressed.mp3";
            long mp3Size = new System.IO.FileInfo(mp3Path).Length;
            long decompressedMp3Size = new System.IO.FileInfo(decompressedMp3Path).Length;

            Assert.True(decompressedMp3Size == mp3Size);


        }
    }

    public class IkeaProblemTest
    {

        [Fact]
        public void IkeaProblemAlgorithm()
        {

            List<Item> items = new List<Item>() {
                new Item("Intermediate Shelf", 1),
                new Item("Cabinet Sides", 2),
                new Item("Top Shelf", 3),
                new Item("Shelf Floor", 4),
                new Item("Cabinet Back Cover", 5),
                new Item("Wall Mount Hinge", 6),
                new Item("Shelf Holder Parts", 7),
                new Item("Furniture Dowels", 8),
                new Item("Screw", 9),
                new Item("Cam Lock Nuts", 10),
                new Item("Middle Shelf",11),

            };

            items[0].Dependencies.Add(8);
            items[1].Dependencies.Add(9);
            items[1].Dependencies.Add(4);
            items[1].Dependencies.Add(11);
            items[1].Dependencies.Add(10);
            items[1].Dependencies.Add(5);
            items[1].Dependencies.Add(7);
            items[2].Dependencies.Add(2);
            items[2].Dependencies.Add(10);
            items[2].Dependencies.Add(6);
            items[3].Dependencies.Add(10);
            items[3].Dependencies.Add(2);
            items[4].Dependencies.Add(9);
            items[6].Dependencies.Add(1);
            items[8].Dependencies.Add(10);
            items[10].Dependencies.Add(2);


            AssemblyGuide assemblyGuide = new AssemblyGuide();
            assemblyGuide.AddItem(items);


            ArrayList assemblySteps = assemblyGuide.GetAssemblySteps();

            List<string> expectedSteps = new List<string>() {
                "Step 1: Attach Furniture Dowels",
                "Step 2: Attach Intermediate Shelf",
                "Step 3: Attach Cam Lock Nuts",
                "Step 4: Attach Screw",
                "Step 5: Attach Shelf Floor",
                "Step 6: Attach Middle Shelf",
                "Step 7: Attach Cabinet Back Cover",
                "Step 8: Attach Shelf Holder Parts",
                "Step 9: Attach Cabinet Sides",
                "Step 10: Attach Wall Mount Hinge",
                "Step 11: Attach Top Shelf",



            };
            ArrayList actualSteps = assemblyGuide.GetAssemblySteps();

            Assert.Equal(expectedSteps, actualSteps.Cast<string>());
        }
    }
}











