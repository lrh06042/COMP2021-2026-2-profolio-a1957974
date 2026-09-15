using System;
using System.Collections.Generic;

public class BankComparer : IComparer<BankAccount>
{
    public int Compare(BankAccount? x, BankAccount? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        // Sort by balance from highest to lowest.
        int result = y.Balance.CompareTo(x.Balance);

        if (result != 0) return result;

        // For equal balances, compare owner names.
        return string.Compare(x.Owner, y.Owner,
            StringComparison.Ordinal);
    }
}