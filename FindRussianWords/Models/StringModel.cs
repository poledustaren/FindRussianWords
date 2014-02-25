using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;


namespace FindRussianWords.Models
{
    public class StringModel
    {
        public string SimpleString { get; set; }
        

        public static IList<string> RegexGo(string regString)
        {
            var pattern = "[А-я ]+";
            StringModel stringModel=new StringModel();
            IList<string> outText=new List<string>();
            
            

            Regex newReg = new Regex(pattern);
            MatchCollection matches = newReg.Matches(regString);

            foreach (Match match in matches)
            {
                var replacaSpace = match.Value.Replace(" ","");
                
                if (!string.IsNullOrWhiteSpace(match.Value)&&replacaSpace.Length>1)
                {
                    outText.Add(match.Value);
                    
                }
            }

            return outText;
            
        }

      
         public static void CreateXml()
         {
             var pathToXml = @"C:\1\123.xml";
             var write = new XmlTextWriter(pathToXml, Encoding.UTF8);
            write.WriteStartDocument();
            write.WriteStartElement("translate");
            write.WriteEndElement();
            write.Close();
         }

        public static void FileNapoln()
        {
            const string pathToXml = @"C:\1\123.xml";
             //var dir = Directory.GetDirectories(@"C:\SVN\z-buro\z-buro\Views");
            var filePath = @"C:\SVN\z_buro_en\z_buro_en\Controllers\MainController.cs";//Directory.GetFiles(@"C:\SVN\z_buro_en\z_buro_en\Views\Shared\Layouts");

            if (filePath is string)
            {
                TakeFileNameWriteXml(filePath, pathToXml);
            }
            else
            {
               /* foreach (var filenameString in filePath)
                {
                    TakeFileNameWriteXml(filenameString, pathToXml);
                }*/
            }
            

            
        }

        public static void TakeFileNameWriteXml(string fileName,string pathToXml)
        {
            var filename = Convert.ToString(fileName).ToLower();
            if (!filename.Contains("jquery") && !filename.Contains("edit") && !filename.Contains("list"))
            {

                XmlDocument document = new XmlDocument();
                document.Load(pathToXml);

                XmlNode element = document.CreateElement("File");
                document.DocumentElement.AppendChild(element); // указываем родителя
                XmlAttribute attribute = document.CreateAttribute("name"); // создаём атрибут
                attribute.Value = Convert.ToString(fileName); // устанавливаем значение атрибута
                element.Attributes.Append(attribute); // добавляем атрибут


                //var stringInTheFile = StringModel.RegexGo(fileName);
                var ind = 0;

                using (var rdr1 = new StreamReader(fileName))
                {
                    var line = rdr1.ReadToEnd();

                    var stringInTheFile = RegexGo(line);

                    foreach (var stringSingle in stringInTheFile)
                    {
                        XmlNode rowElement = document.CreateElement("Row"); // даём имя
                        XmlAttribute rowAttribute = document.CreateAttribute("number");
                        rowAttribute.Value = Convert.ToString(ind);
                        rowElement.Attributes.Append(rowAttribute);
                        element.AppendChild(rowElement); // и указываем кому принадлежит

                        XmlNode rusText = document.CreateElement("Ru");
                        rusText.InnerText = stringSingle;
                        rowElement.AppendChild(rusText);

                        XmlNode engText = document.CreateElement("En");
                        engText.InnerText = " ";
                        rowElement.AppendChild(engText);
                        document.Save(pathToXml);
                        ind++;

                    }
                }
            }
        }

       
        public static void ReadFromXml()
        {
            const string pathToXml = @"C:\Users\Philips-Artsofte\Documents\Visual Studio 2013\Projects\FindRussianWords\FindRussianWords\App_Data\TranslateVer2.xml";
             XDocument doc = XDocument.Load(pathToXml);
            string rusWord;
            string engWord;
            string example = "hello \n";
            example = example.TrimEnd(Environment.NewLine.ToCharArray());
            
            foreach (XElement fileElement in doc.Root.Elements())
            {

                var filename = (string)fileElement.Attribute("name").Value;
                var filenameReplace = filename;//.Replace(@"C:\SVN\z-buro\z-buro", @"C:\SVN\z_buro_en\z_buro_en");
                var viewFile = new StreamReader(filenameReplace);
                string line = viewFile.ReadToEnd();
                var lineReplace="";
                
                foreach (XElement rowElement in fileElement.Elements())
                {
                    rusWord=(string)rowElement.Element("Ru");//rowElement.Attribute("number").ToString();
                    var eng = (string) rowElement.Element("En");
                    engWord=eng.TrimEnd(Environment.NewLine.ToCharArray());
                    if (engWord.Length > 0)
                    {
                        lineReplace = line.Replace(rusWord, engWord);
                        line = lineReplace;
                    }
                    else
                    {
                        lineReplace = line.Replace(rusWord, "needTranslate");
                        line = lineReplace;
                    }
                }

                var filenameReplaceForWrite = filenameReplace.Replace(@"SVN", @"SVN1");


                using (var writeFile = new StreamWriter(filenameReplaceForWrite, false))
                {
                    writeFile.WriteLine(lineReplace);
                }
                
                
                

                
                var b = 0;
                /*//Выводим имя элемента и значение аттрибута id
                Console.WriteLine("{0} {1}", el.Name, el.Attribute("id").Value);
                Console.WriteLine("  Attributes:");
                //выводим в цикле все аттрибуты, заодно смотрим как они себя преобразуют в строку
                foreach (XAttribute attr in el.Attributes())
                    Console.WriteLine("    {0}", attr);
                Console.WriteLine("  Elements:");
                //выводим в цикле названия всех дочерних элементов и их значения
                foreach (XElement element in el.Elements())
                    Console.WriteLine("    {0}: {1}", element.Name, element.Value);*/
            }

        }
    }

        
        
    }
    
