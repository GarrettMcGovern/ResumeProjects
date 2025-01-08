using System.Collections.Generic;



namespace Huffman

{

    public class Program

    {

        static void Main(string[] args)

        {

            string inputFile = File.ReadAllText("wap.txt");

            string outputFilePath = "compression.txt";

            string decompressedFilePath = "decompressed.txt";



            // Calculate frequencies of each character  

            var frequencies = new Dictionary<char, int>();

            foreach (char ch in inputFile)

            {

                if (frequencies.ContainsKey(ch))

                {

                    frequencies[ch]++;

                }

                else

                {

                    frequencies[ch] = 1;

                }

            }



            // Sorted List of character and frequencies  

            HuffmanCoding.BuildFrequencyTable();





            Console.WriteLine();



            // Creates tree 

            HuffmanNode tree = HuffmanCoding.BuildTree(frequencies);





            // Prints codes for each letter  

            var huffmanCodes = new Dictionary<char, string>();

            HuffmanCoding.GenerateCodes(tree, "", huffmanCodes);

            Console.WriteLine("There are " + frequencies.Count + " Different Codes");



            // Compress the text 

            string compressedText = HuffmanCoding.Compress(inputFile, huffmanCodes);



            // Save compressed text to data file 

            HuffmanCoding.WriteToFile(outputFilePath, compressedText);

            Console.WriteLine("Compression complete. Written to " + outputFilePath);



            // Read compressed data from file  

            string compressedFileData = HuffmanCoding.ReadFromFile(outputFilePath);



            // Decompress the data 

            string decompressedText = HuffmanCoding.Decompress(compressedFileData, tree);



            // Save decompressed text to a file  

            File.WriteAllText(decompressedFilePath, decompressedText);

            Console.WriteLine("Decompression complete. Written to " + decompressedFilePath);

        }

    }

}