using System;
using System.Collections.Generic;
using HL7Serializer.Models;
using HL7Serializer.Models.Common;

namespace HL7Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new HLSevenSerializer();

            MessageHeader header;
            Patient patient, mergePatient;
            GetHeaderAndPatients(out header, out patient, out mergePatient);

            TestCreatePatient(header, patient, serializer);
            TestMergePatients(header, patient, mergePatient, serializer);
            TestResponses(serializer);
            TestCreateOrder(serializer, header);
        }

        private static void TestCreateOrder(HLSevenSerializer serializer, MessageHeader header)
        {
            Console.WriteLine("-------------------Create order--------------------");
            header.MessageType = "ORM^O01";
            var orderPatient = new Patient()
            {
                Id = "140Z1093888",
                AssigningAuthority = "KAS",
                BirthDate = new ShortDate(new DateTime(1935, 8, 31)),
                City = "Traunstein",
                Firstname = "Maria",
                Lastname = "Möller",
                MiddleInitialOrName = "Test",
                Suffix = "jun",
                Prefix = "Prof. Dr.",
                Gender = 'F',
                Street = "Testweg 13",
                Country = "DE",
                State = "BY",
                Zip = "89989",
            };

            var order = new Order()
            {
                Code = "NW",
                Number = "404640544",
                NamespaceId = "KAS",
                StartDate = new ShortDate(DateTime.Now),
                TransactionDate = DateTime.Now.AddDays(-1),
                Enterer = new Person()
                {
                    Id = "U873915",
                    Firstname = "Herbert",
                    Lastname = "Leikermoser",
                    MiddleInitialOrName = "F",
                    Prefix = "Dr.",
                    Location = new Location()
                    {
                        PointOfCare = "AUG",
                        Facility = "AUG",
                    },
                },
                Provider = new Person()
                {
                    Id = "03437",
                    Firstname = "Alois",
                    Lastname = "Hinderberger",
                    Prefix = "Dr."
                },
                PhoneNumber = new PhoneNumber()
                {
                    PrimaryNumber = "(216)444-2020",
                    City = "216",
                    SecondaryNumber = "4442020"
                },
                EnteringDevice = new Device()
                {
                    Id = "W28719",
                    Text = "I02-0205-60E96",
                    AlternateId = "100010461000",
                    AlternateText = "OPHT MAIN",
                    NameOfAlternateCodingSystem = "",
                },
                ObservationRequest = new ObservationRequest()
                {
                    Id = "1",
                    OrderNumber = new PlacerOrderNumber()
                    {
                        Id = "404640544",
                        Application = "KAS"
                    },
                    UniversalServiceId = new Device()
                    {
                        Id = "ANGIO",
                        Text = "Fundus Angiography",
                        NameOfCodingSystem = "FORUM",
                        AlternateText = "Fundus Angiography",
                    }
                        ,
                    RequestedDate = new ShortDate(DateTime.Now),
                    OrderingProvider = new Person()
                    {
                        Id = "03437",
                        Firstname = "Helmut",
                        Lastname = "Sixt",
                        MiddleInitialOrName = "J",
                        Prefix = "Dr."
                    },
                    PhoneNumber = new PhoneNumber()
                    {
                        PrimaryNumber = "(216)444-2020",
                        City = "216",
                        SecondaryNumber = "4442020"
                    },
                    QuantityAndTiming = new QuantityAndTiming()
                    {
                        Priority = "R",
                    },
                    Tehnician = new Person()
                    {
                        Id = "0056",
                        Firstname = "Hubert",
                        Lastname = "Obermoser",
                    },
                },
            };
            Console.WriteLine(serializer.Serialize(header, orderPatient, order));
        }

        private static void GetHeaderAndPatients(out MessageHeader header, out Patient patient, out Patient mergePatient)
        {
            header = new MessageHeader()
            {
                SendingApplication = "MEDFLOW",
                SendingFacility = "MEDFLOW",
                ReceivingFacility = "MEDFLOWDCM",
                ReceivingApplication = "MEDFLOW",
                MessageType = "ADT^A01",
                SentDateTime = DateTime.Now
            };
            patient = new Patient()
            {
                Id = "76040",
                AssigningAuthority = "MEDFLOW",
                BirthDate = new ShortDate(new DateTime(1960, 5, 19)),
                City = "MALIBU BEACH",
                Firstname = "Daniel",
                Lastname = "Craig",
                Gender = 'M',
                Street = "6548 SECRET WAY",
            };
            mergePatient = new Patient()
            {
                Id = "123",
                AssigningAuthority = "PROOPT"
            };
        }

        private static void TestMergePatients(MessageHeader header, Patient patient, Patient mergePatient, HLSevenSerializer serializer)
        {
            Console.WriteLine("-------------------Merge patients--------------------");
            Console.WriteLine(serializer.Serialize(header, patient, mergePatient));
        }

        private static void TestCreatePatient(MessageHeader header, Patient patient, HLSevenSerializer serializer)
        {
            Console.WriteLine("------------------Create patient---------------------");
            Console.WriteLine(serializer.Serialize(header, patient));
        }

        private static void TestResponses(HLSevenSerializer serializer)
        {
            var responses = new List<KeyValuePair<bool, string>>()
            {
                new KeyValuePair<bool, string>(true, @"MSH|^~\&|||SAP i.s.h. med|KAS|20140108130546+0100||ACK^A40^ACK|14654726|P|2.4\n
                MSA|AA|14654726||||0^Message accepted"),
                new KeyValuePair<bool, string>(false, @"MSH|^~\&|||MEDFLOW|MEDFLOW|20140108113941+0100||ACK^A01^ACK|0170cd83|P|2.3\n
                            MSA|AR|0170cd83|Combination of patient id and issuer of patient id is not unique, identified by field 'patientId' with fieldValue 'MEDFLOW/76040'!|||205^Duplicate key identifier"),
                new KeyValuePair<bool, string>(true, @"MSH|^~\&|||MEDFLOW|MEDFLOW|20140108113857+0100||ACK^A01^ACK|0170cd83|P|2.3\n
                                    MSA|AA|0170cd83||||0^Message accepted"),
            };

            foreach (var response in responses)
            {
                Console.WriteLine(string.Format("------------------{0} response---------------------", response.Key ? "Success" : "Failed"));
                var deserialized = serializer.Deserialize(response.Value);
                Console.WriteLine(string.Format("Result:\n{0} \n{1}", deserialized.Key, deserialized.Value));
            }
        }
    }
}
