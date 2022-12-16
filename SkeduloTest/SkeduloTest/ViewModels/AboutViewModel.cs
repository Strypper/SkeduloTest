using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SkeduloTest.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Message { get; set; } = "Hi guys, hope all of these meet your requirements I'm currently limited in time so this will devliver as much as possible. \r\n \r\nBut in a serious project I would use CLEAN ARCHITECTURE, that would be too overkilled in this project so I am sticking with the tranditional now, \r\n \r\nI also made list of tasks to sumurize the requirements ";
        public AboutViewModel()
        {
            Title = "About";
            ToDoCommand = new Command(async () => await Shell.Current.GoToAsync("ItemsPage"));
            GoToBreedsCommand = new Command(async () => await Shell.Current.GoToAsync("BreedPage"));
        }

        public ICommand ToDoCommand { get; }
        public ICommand GoToBreedsCommand { get; }
    }
}