using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using LibraryWpfApp.Commands;
using Services;
using System.Windows.Input;
using System.Windows;

namespace LibraryWpfApp.ViewModels
{
    public class BorrowBookDialogViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IBorrowingService _borrowingService;

        public Book BookToBorrow { get; set; }
        public ObservableCollection<Patron> Patrons { get; set; } = new();
        public Patron? SelectedPatron { get; set; }

        public ICommand ConfirmBorrowCommand { get; }

        public BorrowBookDialogViewModel(IBookService bookService, IPatronService patronService, IBorrowingService borrowingService, Book book)
        {
            _bookService = bookService;
            _patronService = patronService;
            _borrowingService = borrowingService;
            BookToBorrow = book;

            LoadPatrons();
            ConfirmBorrowCommand = new RelayCommand(ConfirmBorrow);
        }

        private void LoadPatrons()
        {
            Patrons = new ObservableCollection<Patron>(_patronService.GetAllPatrons());
            SelectedPatron = Patrons.FirstOrDefault();
        }

        private void ConfirmBorrow()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newBorrowing = new BusinessObject.Borrowing
                {
                    BookId = BookToBorrow.BookId,
                    PatronId = SelectedPatron.PatronId,
                    BorrowDate = DateOnly.FromDateTime(DateTime.Now), // ĐÃ SỬA LỖI: Chuyển DateTime sang DateOnly
                    DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)), // ĐÃ SỬA LỖI: Chuyển DateTime sang DateOnly
                    IsReturned = false,
                    Status = "Borrowed"
                };
                _borrowingService.BorrowBook(newBorrowing);

                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows.OfType<Views.BorrowBookDialog>().FirstOrDefault(w => w.DataContext == this)?.DialogResult = true;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Borrow Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
