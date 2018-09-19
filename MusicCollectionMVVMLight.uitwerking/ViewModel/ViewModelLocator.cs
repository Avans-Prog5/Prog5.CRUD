/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MusicCollectionMVVMLight.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MusicCollectionMVVMMVVMLight.Model;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Hier registreren we een SongListViewModel
            //Waarom doen we dit? Denk hier alvast over na.
            //Het antwoord op deze vraag komt over een tijdje.
            SimpleIoc.Default.Register<SongListViewModel>();
        }

        public SongListViewModel SongList
        {
            get
            {
                //De service locator gebruikt een 'singleton' patroon. 
                //Het maakt niet uit hoevaak je een SongList aanvraagt, je krijgt altijd het zelfde object terug. 
                return ServiceLocator.Current.GetInstance<SongListViewModel>();
            }
        }

        public AddSongViewModel AddSong
        {
            get
            {
                //Je kan natuurlijk ook gewoon zelf een ViewModel aanmaken!
                //Hier maken we een addsongViewModel.
                //Deze heeft de List van songs nodig om het aangemaakte liedje aan toe te kunnen voegen. 
                return new AddSongViewModel(this.SongList);
            }
        }

        public EditSongViewModel UpdateSong
        {
            get
            {
                //Hmmm... Wat moeten we hier nu returnen? Denk er maar eens goed over na!
                //Het antwoord: Een EditSongViewModel. 
                //Deze heeft het geselecteerde liedje nodig van de SongListViewModel
                return new EditSongViewModel(SongList.SelectedSong);
            }
        }

        public static void Cleanup()
        {
        }
    }
}