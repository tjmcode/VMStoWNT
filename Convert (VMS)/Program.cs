// <copyright file="Program.cs" company="MicroCODE Incorporated">Copyright © 2018-2020 MicroCODE Incorporated Troy, MI</copyright><author>Timothy J. McGuire</author>

/*
 *      Title:    Program
 *      Module:   Program (MicroCODE:Program.cs)
 *      Project:  MicroCODE VAX/VMS Source Code Restoration
 *      Customer: Internal
 *      Creator:  MicroCODE Incorporated
 *      Date:     August 2021
 *      Author:   Timothy J McGuire
 *
 *      Designed and Coded: 2018-2021 MicroCODE Incorporated
 *
 *      This software and related materials are the property of 
 *      MicroCODE Incorporated and contain confidential and proprietary
 *      information. This software and related materials shall not be
 *      duplicated, disclosed to others, or used in any way without the 
 *      written of MicroCODE Incorported.
 * 
 * 
 *      DESCRIPTION:
 *      ------------
 * 
 *      This module implements the MicroCODE C# Class for 'Convert (VMS)' to convert all our 
 *      VAX/VMS Record Management System (RMS) files into normal WINDOWS TEXT Files for reference.
 *      
 *      The goal: Relive LADDERS Compare for Logix 5000!
 * 
 * 
 * 
 *      REFERENCES:
 *      -----------
 *
 *      1. Internet Search -- VAX/VMS Record Managment System (RMS) File Structure.
 *      
 *      2. MicroHEX - a utility to exmaine raw bytes from a WINDOWS File.
 * 
 * 
 *  
 *      MODIFICATIONS:
 *      --------------
 *
 *  Date:         By-Group:   Rev:     Description:
 *
 *  05-Aug-2021   TJM-MCODE  {0001}    New Tool to convert all VAX/VMS RMS Files to WINDOWS Text Files.
 * 
 * 
 * 
 */

namespace MicroCODE
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;

/// <summary>
/// Class for the Tool "Convert (VMS)".
/// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point and code for the Tool "Convert (VMS)".
        /// </summary>
        public static void Main()
        {
            // change these string to your source and destination root folders...
            string vmsFolder = @"D:\MicroCODE\VAX\MDS\";
            string wntFolder = @"D:\MicroCODE\MDS\";

            // Show progress
            Console.WriteLine(string.Format("Converting TREE: {0} to: {1}", vmsFolder, wntFolder));

            // Convert all files in the Root Directory
            ConvertFiles(vmsFolder, wntFolder);

            // Show progress
            Console.WriteLine(string.Format("Conversion COMPLETED."));
        }

        /// <summary>
        /// Converts all VAX/VMS Files in a given folder into WNT Files in a mirrored Folder.
        /// </summary>
        /// <param name="vmsFolder">Path to the VAX/VMS Files.</param>
        /// <param name="wntFolder">Path to the WNT Text Files.</param>
        private static void ConvertFiles(string vmsFolder, string wntFolder)
        {
            // Show progress
            Console.WriteLine(string.Format("Converting FOLDER: {0} to: {1}", vmsFolder, wntFolder));

            // Make sure the directory exists first... (it may be EMPTY)
            Directory.CreateDirectory(wntFolder);

            // Proces all files in "vmsFolder"
            string[] vmsFiles = Directory.GetFiles(vmsFolder);

            // For each file, convert from VAX/VMS to WNT
            foreach (var vmsFile in vmsFiles)
            {
                ConvertFile(vmsFolder, wntFolder, Path.GetFileName(vmsFile));
            }

            // Get list of all Sub-Directories requiring processing
            string[] vmsFolders = Directory.GetDirectories(vmsFolder);

            // RECURSE: For each folder, convert from VAX/VMS to WNT
            foreach (var vmsSubFolder in vmsFolders)
            {
                string wntSubFolder = CopySwapSubstring(vmsSubFolder, vmsFolder, wntFolder);

                ConvertFiles(vmsSubFolder + @"\", wntSubFolder + @"\");
            }
        }

        /// <summary>
        /// Convert a single file from VAX/VMS RMS format to a WINDOWS NT Text File.
        /// </summary>
        /// <param name="vmsFolder">Folder holding the VAX/VMS RMS File.</param>
        /// <param name="wntFolder">Folder to hold the new WINDOWS NT Text file.</param>
        /// <param name="fileName">File to be converted.</param>
        private static void ConvertFile(string vmsFolder, string wntFolder, string fileName)
        {
            FileStream vmsFile = null;
            BinaryReader vmsReader = null;

            FileStream wntFile = null;
            StreamWriter wntWriter = null;

            int byteIndex = 0;
            int lineIndex;
            int bcw, bcwLoByte, bcwHiByte;
            string line;

            try
            {
                // Show progress
                Console.WriteLine(string.Format("Converting FILE: {0}", vmsFolder + fileName));

                // Make sure the directory exists first...
                Directory.CreateDirectory(wntFolder);

                // Open/Create File for Import
                vmsFile = new FileStream(vmsFolder + fileName, FileMode.Open, FileAccess.Read);

                // Create the reader for data
                vmsReader = new BinaryReader(vmsFile);

                // Open/Create File for Export
                wntFile = new FileStream(wntFolder + fileName, FileMode.Create, FileAccess.Write);

                // Create the writer for data
                wntWriter = new StreamWriter(wntFile);

                while (byteIndex < vmsReader.BaseStream.Length)
                {
                    // test for ODD index
                    if ((byteIndex % 2) != 0)  
                    {
                        vmsReader.ReadByte();
                        byteIndex++;  // skip padding character - should be NULL, but is stop guaranteed to be.
                    }

                    if (byteIndex >= vmsReader.BaseStream.Length)
                    {
                        break;  // skipping PAD character reached EOF.
                    }

                    lineIndex = byteIndex;
                    bcwLoByte = vmsReader.ReadByte();
                    byteIndex++;

                    if (byteIndex >= vmsReader.BaseStream.Length)
                    {
                        break;  // Reading low byte of BCW reached EOF.
                    }

                    // pick up hi byte of Byte-Count-Word (BCW)
                    bcwHiByte = vmsReader.ReadByte();
                    byteIndex++;

                    if (byteIndex >= vmsReader.BaseStream.Length)
                    {
                        break;  // reading high byte of BCW reached EOF.
                    }

                    // create BCW from Lo and Hi Bytes
                    bcw = (bcwHiByte << 8) | bcwLoByte;

                    // create next line of output file
                    line = string.Empty;

                    //// NOTE: a BCW of ZERO = a Blank Line, a legal RMS record.

                    if (bcw == 0xFFFF)
                    {
                        break;  // End-of-File (EOF)
                    }
                    if (bcw > 1024)
                    {
                        wntWriter.Write(string.Format("\n"));
                        wntWriter.Write(string.Format("BCW={0:D4} SOL={1:X6} ERROR: Line over maximum size!\n", bcw, lineIndex));  // Debug output
                        break;
                    }
                    else
                    {
                        // read all bytes counted by BCW and append to next line
                        for (int i = 0; i < bcw; i++)
                        {
                            line += Convert.ToChar(vmsReader.ReadByte());
                            byteIndex++;

                            if (byteIndex >= vmsReader.BaseStream.Length)
                            {
                                break;  // reading bytes of BCW reached EOF.
                            }
                        }
#if TESTING
                        // write used for debugging issues... (replaces the line above)
                        sw.Write(string.Format("BCW={0:D4} SOL={1:X6} LINE={2}\n", bcw, lineIndex, line));  // Debug output
#else
                        // write converted line of VMS / Record Management System (RMS) File to Windows NT Text File
                        wntWriter.WriteLine(line);
#endif
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
            finally
            {
                // Close Stream & File, if open
                wntWriter?.Close();
                wntFile?.Close();

                vmsReader?.Close();
                vmsFile?.Close();
            }
        }

        //// some methods from MicroCODE's "ASCII" Class...

        /// <summary>
        /// Returns a string copy of the original with all 'old substrings' converted to 'new substrings'.
        /// </summary>
        /// <param name="objectText">Text to be selectively copied.</param>
        /// <param name="oldSubstring">Character to be converted to 'new'.</param>
        /// <param name="newSubstring">Character to replace 'old'.</param>
        /// <returns>String, of same length as the original, with all 'old characters' converted to 'new characters'.</returns>
        private static string CopySwapSubstring(string objectText, string oldSubstring, string newSubstring)
        {
            if (objectText == null)
            {
                return string.Empty;
            }

            StringBuilder convertedString = new StringBuilder();
            int srcIndex, dstIndex = 0;
            int desiredLength = objectText.Length;

            for (srcIndex = 0; (dstIndex < (0 + desiredLength)) && (srcIndex < objectText.Length);)
            {
                if (Compare(oldSubstring, objectText, srcIndex, true))
                {
                    convertedString.Append(newSubstring);
                    dstIndex += newSubstring.Length;
                    srcIndex += oldSubstring.Length;
                }
                else
                {
                    convertedString.Append(objectText[srcIndex++]);
                    dstIndex++;
                }
            }

            return convertedString.ToString();
        }

        /// <summary>
        /// Compares a sub-string to a location in a character array, using selected case sensitivity.
        /// </summary>
        /// <param name="objectText">The string being looked for.</param>
        /// <param name="sourceText">The character array where it may reside.</param>
        /// <param name="startingIndex">The starting index in the source string.</param>
        /// <param name="caseSensitive">A flag indicating the text compare needs to be case sensitive.</param>
        /// <returns>A value indicating whether or not the substring is in the character array at the position in question.</returns>
        private static bool Compare(string objectText, string sourceText, int startingIndex, bool caseSensitive)
        {
            int objectIndex;
            int sourceIndex = startingIndex;
            bool status = false;

            if ((objectText != null) && (sourceText != null) && (objectText.Length != 0) && (sourceText.Length != 0))
            {
                for (objectIndex = 0; (objectIndex < objectText.Length) && (sourceIndex < sourceText.Length); sourceIndex++, objectIndex++)
                {
                    char s = (caseSensitive) ? objectText[objectIndex] : ToLower(objectText[objectIndex]);
                    char m = (caseSensitive) ? sourceText[sourceIndex] : ToLower(sourceText[sourceIndex]);

                    if (s != m)
                    {
                        break;  // not equal
                    }
                }

                if (objectIndex == objectText.Length)
                {
                    status = true;  // the entire subString was found at the startingIndex within the mainString
                }
            }
            return status;
        }

        /// <summary>
        /// Convert a single character from Upper Case to Lower Case.
        /// </summary>
        /// <param name="objectByte">The character to convert.</param>
        /// <returns>The character, converted to lower case if applicable.</returns>
        private static char ToLower(char objectByte)
        {
            if (IsAlphaUpper(objectByte))
            {
                return (char)(objectByte + (97 - 65));  // a - A
            }
            else
            {
                return objectByte;
            }
        }

        /// <summary>
        /// Tests a single character for A-Z range.
        /// </summary>
        /// <param name="objectByte">Character to be tested.</param>
        /// <returns>A value indicating whether or not the character is 'capitalized alphabet letter'.</returns>
        private static bool IsAlphaUpper(char objectByte)
        {
            return ((objectByte >= 65) && (objectByte <= 90)) ? true : false;  // >= A, <= Z
        }
    }
}
