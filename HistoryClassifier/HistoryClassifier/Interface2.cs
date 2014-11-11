using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryClassifier
{
    
    interface Classification
    {

        string Get_String();
        int Get_Int();
    }

    class Ad : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public Ad()
        {
            str_type = "Advertisement";
            enum_type = desc.Ads;
            int_type = 4;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class Bug : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public Bug()
        {
            str_type = "Bug";
            enum_type = desc.Bug;
            int_type = 0;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }


    class Enhancement : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public Enhancement()
        {
            str_type = "Enhancement";
            enum_type = desc.Enhancement;
            int_type = 2;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }


    class Feature : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public Feature()
        {
            str_type = "Feature";
            enum_type = desc.Feature;
            int_type = 1;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class Junk : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public Junk()
        {
            str_type = "Junk";
            enum_type = desc.Junk;
            int_type = 3;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class NotClassified : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public NotClassified()
        {
            str_type = "Not Classified";
            enum_type = desc.NotClassified;
            int_type = 6;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }

    class RevChangeRequest : Classification
    {
        public string str_type;
        public desc enum_type;
        public int int_type;

        public RevChangeRequest()
        {
            str_type = "Review Change Request";
            enum_type = desc.RevChangeRequest;
            int_type = 5;
        }

        public string Get_String()
        {
            return str_type;
        }

        public int Get_Int()
        {
            return int_type;
        }

    }


}
