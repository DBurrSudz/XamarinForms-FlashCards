using FlashCards.Views;
using Xamarin.Forms;

namespace FlashCards
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Registration), typeof(Registration));
            Routing.RegisterRoute(nameof(CreateGroupModal),typeof(CreateGroupModal));
            Routing.RegisterRoute(nameof(EditGroupModal),typeof(EditGroupModal));
            Routing.RegisterRoute(nameof(Topics), typeof(Topics));
            Routing.RegisterRoute(nameof(CreateTopicModal),typeof(CreateTopicModal));
            Routing.RegisterRoute(nameof(EditTopicModal),typeof(EditTopicModal));
            Routing.RegisterRoute(nameof(Cards), typeof(Cards));
            Routing.RegisterRoute(nameof(CreateCardModal),typeof(CreateCardModal));
            Routing.RegisterRoute(nameof(EditCardModal),typeof(EditCardModal)); 
        }

    }
}
