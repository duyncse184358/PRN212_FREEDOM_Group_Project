﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Commands;
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.ViewModels
{
    public class FinePaymentDialogViewModel : BaseViewModel
    {
        private readonly IFineService _fineService;
        private readonly IPatronService _patronService;

        public int BorrowingID { get; set; }
        public int PatronID { get; set; }
        public string PatronName { get; set; } = string.Empty;
        public decimal FineAmount { get; set; }
        public bool IsPaid { get; set; }

        public ICommand ProcessPaymentCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public FinePaymentDialogViewModel() : this(
            (Application.Current as App)?.Services.GetRequiredService<IFineService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!
        )
        {
        }

        public FinePaymentDialogViewModel(IFineService fineService, IPatronService patronService)
        {
            _fineService = fineService;
            _patronService = patronService;

            ProcessPaymentCommand = new RelayCommand(ProcessPayment);
        }

        // ĐÃ THÊM LẠI PHƯƠNG THỨC SETUP NÀY
        public void Setup(int borrowingId, int patronId, decimal amount, string patronName)
        {
            BorrowingID = borrowingId;
            PatronID = patronId;
            FineAmount = amount;
            PatronName = patronName;
            IsPaid = false; // Mặc định là chưa trả khi setup
            OnPropertyChanged(nameof(BorrowingID));
            OnPropertyChanged(nameof(PatronID));
            OnPropertyChanged(nameof(FineAmount));
            OnPropertyChanged(nameof(PatronName));
            OnPropertyChanged(nameof(IsPaid));
        }

        private void ProcessPayment()
        {
            try
            {
                var existingFine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == BorrowingID && f.PatronId == PatronID);

                if (existingFine != null)
                {
                    if (existingFine.Paid == null || existingFine.Paid == false)
                    {
                        _fineService.PayFine(existingFine.FineId);
                        IsPaid = true; // Cập nhật trạng thái trong ViewModel
                        OnPropertyChanged(nameof(IsPaid));
                        MessageBox.Show("Existing fine payment processed successfully!", "Payment Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("This fine has already been paid.", "Already Paid", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    var newFine = new BusinessObject.Fine
                    {
                        BorrowingId = BorrowingID,
                        PatronId = PatronID,
                        FineAmount = FineAmount,
                        Paid = true,
                        FineDate = DateTime.Now
                    };
                    _fineService.AddFine(newFine);
                    MessageBox.Show("New fine record and payment processed successfully!", "Payment Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                var dialog = Application.Current.Windows.OfType<Views.FinePaymentDialog>().FirstOrDefault(w => w.DataContext == this);
                if (dialog != null)
                {
                    dialog.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Payment Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
