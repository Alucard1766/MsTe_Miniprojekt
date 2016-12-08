using AutoReservation.Common.DataTransferObjects.Core;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase<AutoDto>
    {
        private int basistarif;
        [DataMember]
        public int Basistarif
        {
            get { return basistarif; }
            set
            {
                if (basistarif != value)
                {
                    basistarif = value;
                    OnPropertyChanged(nameof(Basistarif));
                }
            }
        }

        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string marke;
        [DataMember]
        public string Marke
        {
            get { return marke; }
            set
            {
                if (marke != value)
                {
                    marke = value;
                    OnPropertyChanged(nameof(Marke));
                }
            }
        }

        private byte[] rowVersion;
        [DataMember]
        public byte[] RowVersion
        {
            get { return rowVersion; }
            set
            {
                if (rowVersion != value)
                {
                    rowVersion = value;
                    OnPropertyChanged(nameof(RowVersion));
                }
            }
        }

        private int tagestarif;
        [DataMember]
        public int Tagestarif
        {
            get { return tagestarif; }
            set
            {
                if (tagestarif != value)
                {
                    tagestarif = value;
                    OnPropertyChanged(nameof(Tagestarif));
                }
            }
        }

        private AutoKlasse autoKlasse;
        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get { return autoKlasse; }
            set
            {
                if (autoKlasse != value)
                {
                    autoKlasse = value;
                    OnPropertyChanged(nameof(AutoKlasse));
                }
            }
        }




        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString() => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}";

    }
}
