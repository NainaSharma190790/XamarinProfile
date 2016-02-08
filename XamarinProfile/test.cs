using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XamarinProfile
{
	public class TesteViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public TesteViewModel()
		{
			ListaEstados = new ObservableCollection<string>();
			ListaEstados.Add("DF");
			ListaEstados.Add("SP");
			ListaEstados.Add("RJ");
			ListaEstados.Add("BH");
			ListaEstados.Add("RS");

			Pegar = new Command(OnPegar);
		}

		private void OnPegar(object obj)
		{
			if (!string.IsNullOrEmpty(Estado))
			{
				EstadoSelecionado = Estado;
			}
			else
			{
				EstadoSelecionado = "Selecione um Estado";
			}
		}

		public ICommand Pegar { get; set; }

		ObservableCollection<string> _listaEstados;
		public ObservableCollection<string> ListaEstados
		{
			get
			{
				return _listaEstados;
			}
			set
			{
				_listaEstados = value;
				OnPropertyChanged();
			}
		}

		string _estado;
		public string Estado
		{
			get
			{
				return _estado;
			}
			set
			{
				_estado = value;
				OnPropertyChanged();
			}
		}
		string _estadoSelecionado;
		public string EstadoSelecionado
		{
			get
			{
				return _estadoSelecionado;
			}
			set
			{
				_estadoSelecionado = value;
				OnPropertyChanged();
			}
		}
	}
}