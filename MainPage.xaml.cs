using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace MauiCalculator
{


    //using System.ComponentModel;
    //using System.Runtime.CompilerServices;
    //using Microsoft.Maui.Controls;


    //    public partial class MainPage : ContentPage, INotifyPropertyChanged
    //    {
    //        private string _result;

    //        public string Result
    //        {
    //            get => _result;
    //            set
    //            {
    //                _result = value;
    //                OnPropertyChanged();
    //            }
    //        }

    //        public Command NumberButtonPressed { get; }
    //        public Command OperatorButtonPressed { get; }
    //        public Command ClearButtonPressed { get; }
    //        public Command EqualsButtonPressed { get; }

    //        private decimal _operand1;
    //        private decimal _operand2;
    //        private char _operator;

    //        public MainPage()
    //        {



    //        InitializeComponent();
    //        NumberButtonPressed = new Command<string>((number) =>
    //            {
    //                if (_operator == '\0')
    //                {
    //                    _operand1 = _operand1 * 10 + decimal.Parse(number);
    //                    Result = _operand1.ToString();
    //                }
    //                else
    //                {
    //                    _operand2 = _operand2 * 10 + decimal.Parse(number);
    //                    Result = _operand2.ToString();
    //                }
    //            });

    //            OperatorButtonPressed = new Command<string>((op) =>
    //            {
    //                _operator = op[0];
    //            });

    //            ClearButtonPressed = new Command(() =>
    //            {
    //                _operand1 = 0;
    //                _operand2 = 0;
    //                _operator = '\0';
    //                Result = "0";
    //            });

    //            EqualsButtonPressed = new Command(() =>
    //            {
    //                switch (_operator)
    //                {
    //                    case '+':
    //                        _operand1 += _operand2;
    //                        break;
    //                    case '-':
    //                        _operand1 -= _operand2;
    //                        break;
    //                    case '*':
    //                        _operand1 *= _operand2;
    //                        break;
    //                    case '/':
    //                        _operand1 /= _operand2;
    //                        break;
    //                }
    //                Result = _operand1.ToString();
    //                _operand2 = 0;
    //                _operator = '\0';
    //            });
    //        }

    //        public event PropertyChangedEventHandler PropertyChanged;

    //        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //        {
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //}



    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
       
        double number = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand MultiplyBy2Command { get; private set; }
        public ICommand DivideBy2Command { get; private set; }

        //public CommandDemoViewModel()
        //{
           
        //}

        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
                }
            }


        }
        public MainPage()
        {
            InitializeComponent();


            this.BindingContext = new CommandDemoViewModel();

            //MultiplyBy2Command = new Command(() => Number *= 2);
            //DivideBy2Command = new Command(() => Number /= 2);

            //MultiplyBy2Command = new Command(
            //execute: () =>
            //{
            //    Number *= 2;
            //    ((Command)MultiplyBy2Command).ChangeCanExecute();
            //    ((Command)DivideBy2Command).ChangeCanExecute();
            //},
            //canExecute: () => Number < System.Math.Pow(2, 10));

            //DivideBy2Command = new Command(
            //    execute: () =>
            //    {
            //        Number /= 2;
            //        ((Command)MultiplyBy2Command).ChangeCanExecute();
            //        ((Command)DivideBy2Command).ChangeCanExecute();
            //    },
            //    canExecute: () => Number > System.Math.Pow(2, -10));

            
        }

     
        //public event PropertyChangedEventHandler PropertyChanged;
    }


    public class CommandDemoViewModel : INotifyPropertyChanged
    {

        private string inputText = "0";
        private double result = 0;
        private string lastOperator = "";
        private bool isNewInput = true;

        public string InputText
        {
            get { return inputText; }
            set
            {
                if (inputText != value)
                {
                    inputText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputText"));
                }
            }
        }




        public ICommand InputCommand { get; private set; }
        public ICommand OperatorCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand CalculateCommand { get; private set; }


        double number = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand MultiplyBy2Command { get; private set; }
        public ICommand DivideBy2Command { get; private set; }

        public CommandDemoViewModel()
        {
            MultiplyBy2Command = new Command(() => Number *= 2);
            DivideBy2Command = new Command(() => Number /= 2);



            InputCommand = new Command<string>((input) =>
            {
                if (isNewInput)
                {
                    InputText = input;
                    isNewInput = false;
                }
                else
                {
                    InputText += input;
                }
            });

            OperatorCommand = new Command<string>((op) =>
            {
                if (lastOperator == "")
                {
                    result = double.Parse(InputText);
                }
                else
                {
                    result = Calculate(lastOperator, result, double.Parse(InputText));
                    InputText = result.ToString();
                }

                lastOperator = op;
                isNewInput = true;
            });

            ClearCommand = new Command(() =>
            {
                InputText = "0";
                result = 0;
                lastOperator = "";
                isNewInput = true;
            });

            CalculateCommand = new Command(() =>
            {
                result = Calculate(lastOperator, result, double.Parse(InputText));
                InputText = result.ToString();
                lastOperator = "";
                isNewInput = true;
            });

        }


        private double Calculate(string op, double num1, double num2)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    return num1 / num2;
                default:
                    return 0;
            }
        }



        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
                }
            }
        }



        //public event PropertyChangedEventHandler PropertyChanged;
    }



}
