# Business requirements — read this before changing code

You're joining a team maintaining a tiny order checkout engine. A teammate reports incorrect totals and stock occasionally disappearing after rejected reservations. Reproduce and fix the issues **without weakening the requirements or tests**.

1. `Subtotal` = sum of `Quantity * UnitPrice` for all lines.
2. `Discount` = `Subtotal * discountPercent / 100` (decimal arithmetic). Allowed percentage: 0 to 100 inclusive.
3. `Discounted subtotal` = subtotal - discount.
4. Shipping costs **7.50** when the discounted subtotal is **strictly less than 50.00**; otherwise shipping is free.
5. `Total` = discounted subtotal + shipping.
6. Quantities must be positive, prices nonnegative, SKUs nonempty.
7. `TryReserve` succeeds only when every requested SKU has enough stock.
8. Rejected reservations **must not change any stock** (all-or-nothing).
9. Duplicate SKU lines count together, case-insensitively. Example: A:3 + a:3 requires six units.

## Acceptance criteria

- All tests pass and existing valid behavior remains intact.
- Fix production code in `src`, not expected values in the tests.
- Explain each root cause, not just its symptom.
- Optional: add edge-case tests for empty orders, 100% discounts and zero-price items.
