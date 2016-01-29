using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamlBrewer.Uwp.EnumSliderSample.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private Importance importance; //  = Importance.Critical; // Does work.

        public MainPageViewModel()
        {
            updateCommand = new DelegateCommand(Update_Executed);
        }

        public Importance Importance
        {
            get { return importance; }
            set { SetProperty(ref importance, value); }
        }

        private ICommand updateCommand;

        public ICommand UpdateCommand
        {
            get { return updateCommand; }
        }

        private void Update_Executed()
        {
            Importance = Importance.Critical;
        }
    }

    // [Flags] Does work.
    enum Importance
    {
        None, // = -1, // Does not work.
        Trivial,
        [DefaultValue(true)] // No reaction. But that's not the Slider's fault.
        Moderate,
        Important, // = 10,
        [Display(Name = "O M G")]
        Critical
    };
}
