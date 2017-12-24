using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.ViewModels
{
    class SurveyViewModel : BaseViewModel
    {
        public SurveyViewModel()
        {
            Title = "Survey";

        }

        public bool _q1;
        public bool q1   
        {
            get { return _q1; }
            set { _q1 = value; OnPropertyChanged(); }
        }

        public bool _q2;
        public bool q2
        {
            get { return _q2; }
            set { _q2 = value; OnPropertyChanged(); }
        }

        public bool _q3;
        public bool q3
        {
            get { return _q3; }
            set { _q3 = value; OnPropertyChanged(); }
        }

        public bool _q4;
        public bool q4
        {
            get { return _q4;  }
            set { _q4 = value; OnPropertyChanged(); }
        }

        public bool _q5;
        public bool q5
        {
            get { return _q5; }
            set { _q5 = value; OnPropertyChanged(); }
        }

        public bool _q6;
        public bool q6
        {
            get { return _q6; }
            set { _q6 = value; OnPropertyChanged(); }
        }

        public bool _q7;
        public bool q7
        {
            get { return _q7; }
            set { _q7 = value; OnPropertyChanged(); }
        }

        public bool _q8;
        public bool q8
        {
            get { return _q8; }
            set { _q8 = value; OnPropertyChanged(); }
        }

        public bool _q9;
        public bool q9
        {
            get { return _q9; }
            set { _q9 = value; OnPropertyChanged(); }
        }

        public bool _q10;
        public bool q10
        {
            get { return _q10; }
            set { _q10 = value; OnPropertyChanged(); }
        }

        public bool _q11;
        public bool q11
        {
            get { return _q11; }
            set { _q11 = value; OnPropertyChanged(); }
        }

        public bool _q12;
        public bool q12
        {
            get { return _q12; }
            set { _q12 = value; OnPropertyChanged(); }
        }

        public bool _q13;
        public bool q13
        {
            get { return _q13; }
            set { _q13 = value; OnPropertyChanged(); }
        }

    }
}
