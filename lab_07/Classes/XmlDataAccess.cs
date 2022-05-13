using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace lab_07.Classes
{
    // Implementacja interfejsu korzystająca z lokalnego pliku xml
    public class XmlDataAccess : IDataAccess
    {
        private string _fileName = "lab_07.xml";
        private string _filePath;

        public XmlDataAccess()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _fileName);

            if (!File.Exists(_filePath))
            {
                XElement userEntitesNode = new XElement("UserEntities");

                userEntitesNode.Save(_filePath);
            }
        }

        public List<UserEntity> GetAll()
        {
            //XElement users = XElement.Load(_filePath);

            //IEnumerable<UserEntity> usersQuery = from UserEntity user in users()
            //                                     where user.RemovedAt.HasValue == false
            //                                     select user;

            //List<UserEntity> userEntities = usersQuery.ToList();

            return null;
        }


        public void AddNew(UserEntity userEntity)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserEntity));

            using (FileStream fileStream = File.Create(_filePath))
            {
                xmlSerializer.Serialize(fileStream, userEntity);
            }

            //XElement users = XElement.Load(_filePath);

            //XmlSerializer xmlSerializer = new StringReader(typeof(UserEntity));

            //using (TextReader textReader = new FileReader(_filePath))
            //{

            //}

            //var x = XmlReader.Create(_filePath);

            //xmlSerializer.Serialize()

            //users.Add(userEntity);
            //XmlDocument x = new XmlDocument();

            //users.Save(_filePath);
        }

        public void Edit(long id, UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}