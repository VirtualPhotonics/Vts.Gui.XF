using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Vts.Gui.XF.Models;
using Vts.Gui.XF.View;

namespace Vts.Gui.XF.ViewModel
{
    public class PanelsListViewModel : BaseViewModel
    {
        public List<PanelsItem> PanelsList { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PanelsListViewModel()
        {
            PanelsList = new List<PanelsItem>
            {
                new PanelsItem { Id = Guid.NewGuid().ToString(), Name = "Spectral Panel", Description="Construct absorption and reduced scattering coefficient spectra." },
                new PanelsItem { Id = Guid.NewGuid().ToString(), Name = "Second item", Description="This is an item description." },
  
            };

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                PanelsList.Clear();
                //var panels = await DataStore.GetItemsAsync(true);
                //foreach (var panel in panels)
                //{
                //    PanelsList.Add(panel);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}