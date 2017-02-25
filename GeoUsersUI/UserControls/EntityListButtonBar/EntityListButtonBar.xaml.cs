using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for EntityListButtonBar.xaml
    /// </summary>
    public partial class EntityListButtonBar : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static readonly DependencyProperty DeleteFunctionProperty = DependencyProperty.Register("DeleteFunction",
                                                                                                       typeof(Func<Task<bool>>),
                                                                                                       typeof(EntityListButtonBar),
                                                                                                       new PropertyMetadata());

        public Func<Task<bool>> DeleteFunction
        {
            get
            {
                return (Func<Task<bool>>)GetValue(DeleteFunctionProperty);
            }
            set
            {
                SetValue(DeleteFunctionProperty, value);

                OnPropertyChanged(nameof(DeleteFunction));
            }
        }

        public static readonly DependencyProperty EditFunctionProperty = DependencyProperty.Register("EditFunction",
                                                                                                     typeof(Func<Task<bool>>),
                                                                                                     typeof(EntityListButtonBar),
                                                                                                     new PropertyMetadata());

        public Func<Task<bool>> EditFunction
        {
            get
            {
                return (Func<Task<bool>>)GetValue(EditFunctionProperty);
            }
            set
            {
                SetValue(EditFunctionProperty, value);

                OnPropertyChanged(nameof(EditFunction));
            }
        }

        public static readonly DependencyProperty CreateFunctionProperty = DependencyProperty.Register("CreateFunction",
                                                                                                        typeof(Func<Task<bool>>),
                                                                                                        typeof(EntityListButtonBar),
                                                                                                        new PropertyMetadata());

        public Func<Task<bool>> CreateFunction
        {
            get
            {
                return (Func<Task<bool>>)GetValue(CreateFunctionProperty);
            }
            set
            {
                SetValue(CreateFunctionProperty, value);

                OnPropertyChanged(nameof(CreateFunction));
            }
        }

        public static readonly DependencyProperty CloseFunctionProperty = DependencyProperty.Register("CloseFunction",
                                                                                                      typeof(Func<bool>),
                                                                                                      typeof(EntityListButtonBar),
                                                                                                      new PropertyMetadata());

        public Func<bool> CloseFunction
        {
            get
            {
                return (Func<bool>)GetValue(CloseFunctionProperty);
            }
            set
            {
                SetValue(CloseFunctionProperty, value);

                OnPropertyChanged(nameof(CloseFunction));
            }
        }

        public EntityListButtonBar()
        {
            InitializeComponent();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            await EditFunction();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await DeleteFunction();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseFunction();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateFunction();
        }
    }
}
