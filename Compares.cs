using System;
namespace BTM
{
    public class LineComparer : IComparer<ILine>
    {
        int sign;
        string field;

        public LineComparer(string sign, string field)
        {
            this.sign = checkSign(sign);
            this.field = field;
        }


        public int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool numberDecCompare(ILine? x, ILine? y)
        {
            if (x == null || y == null) return false;
            return x.NumberDec.CompareTo(y.NumberDec) == sign;
        }

        public bool numberHexCompare(ILine? x, ILine? y)
        {
            if (x == null || y == null) return false;
            return x.NumberHex.CompareTo(y.NumberHex) == sign;
        }

        public bool commonNameCompare(ILine ? x, ILine? y)
        {
            if (x == null || y == null) return false;
            return x.CommonName.CompareTo(y.CommonName) == sign;
        }


        public bool compare(ILine? x, ILine? y)
        {
            switch(field)
            {
                case "numberdec":
                    return numberDecCompare(x, y);
                case "numberhex":
                    return numberHexCompare(x, y);
                case "commonName":
                    return commonNameCompare(x, y);
                default:
                    return false;
            }
        }
    }

    public class StopComparer : IComparer<IStop>
    {
        int sign;
        string field;

        public StopComparer(string sign, string field)
        {
            this.sign = checkSign(sign);
            this.field = field;
        }


        public int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool idCompare(IStop? x, IStop? y)
        {
            if (x == null || y == null) return false;
            return x.Id.CompareTo(y.Id) == sign;
        }

        public bool nameCompare(IStop? x, IStop? y)
        {
            if (x == null || y == null) return false;
            return x.Name.CompareTo(y.Name) == sign;
        }

        public bool typeCompare(IStop? x, IStop? y)
        {
            if (x == null || y == null) return false;
            return x.Type.CompareTo(y.Type) == sign;
        }


        public bool compare(IStop? x, IStop? y)
        {
            switch (field)
            {
                case "id":
                    return idCompare(x, y);
                case "name":
                    return nameCompare(x, y);
                case "type":
                    return typeCompare(x, y);
                default:
                    return false;
            }
        }
    }

    public class VehicleComparer : IComparer<IVehicle>
    {
        int sign;
        string field;

        public VehicleComparer(string sign, string field)
        {
            this.sign = checkSign(sign);
            this.field = field;
        }


        public int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool idCompare(IVehicle? x, IVehicle? y)
        {
            if (x == null || y == null) return false;
            return x.Id.CompareTo(y.Id) == sign;
        }

        public bool compare(IVehicle? x, IVehicle? y)
        {
            switch (field)
            {
                case "id":
                    return idCompare(x, y);
                default:
                    return false;
            }
        }
    }
    public class DriverComparer : IComparer<IDriver>
    {
        int sign;
        string field;

        public DriverComparer(string sign, string field)
        {
            this.sign = checkSign(sign);
            this.field = field;
        }


        public int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool nameCompare(IDriver? x, IDriver? y)
        {
            if (x == null || y == null) return false;
            return x.Name.CompareTo(y.Name) == sign;
        }

        public bool surnameCompare(IDriver? x, IDriver? y)
        {
            if (x == null || y == null) return false;
            return x.Surname.CompareTo(y.Surname) == sign;
        }
        public bool seniorityCompare(IDriver? x, IDriver? y)
        {
            if (x == null || y == null) return false;
            return x.Seniority.CompareTo(y.Seniority) == sign;
        }
        public bool compare(IDriver? x, IDriver? y)
        {
            switch (field)
            {
                case "name":
                    return nameCompare(x, y);
                case "surname":
                    return surnameCompare(x, y);
                case "seniority":
                    return seniorityCompare(x, y);
                default:
                    return false;
            }
        }
    }

}

