﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace XamarinCodeLibrary.Views.UserControls.MapViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeocoderPage : ContentPage
    {
        Geocoder geoCoder;

        public GeocoderPage()
        {
            InitializeComponent();
            geoCoder = new Geocoder();
        }

        private async void OnGeocodeButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(geocodeEntry.Text))
            {
                string address = geocodeEntry.Text;
                IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                Position position = approximateLocations.FirstOrDefault();
                geocodedOutputLabel.Text = $"{position.Latitude}, {position.Longitude}";
            }
        }

        private async void OnReverseGeocodeButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(reverseGeocodeEntry.Text))
            {
                string[] coordinates = reverseGeocodeEntry.Text.Split(',');
                double? latitude = Convert.ToDouble(coordinates.FirstOrDefault());
                double? longitude = Convert.ToDouble(coordinates.Skip(1).FirstOrDefault());

                if (latitude != null && longitude != null)
                {
                    Position position = new Position(latitude.Value, longitude.Value);
                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    string address = possibleAddresses.FirstOrDefault();
                    reverseGeocodedOutputLabel.Text = address;
                }
            }

        }
    }
}