# FLOWCHART DETAILED DESCRIPTIONS
## For Creating Visual Flowcharts in Word/PowerPoint/Draw.io

---

## FLOWCHART 1: User Login Process

### Symbol Legend:
- 🔵 Oval = Start/End (Terminal)
- ⬛ Rectangle = Process/Action
- 🔶 Diamond = Decision
- ⬜ Parallelogram = Input/Output

### Flowchart Structure:

```
┌─────────────────┐
│   🔵 START     │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ Display Login Form  │
└────────┬────────────────┘
         │
         ▼
┌────────────────────────────────────┐
│ ⬜ User enters Username & Password │
└────────┬───────────────────────────┘
         │
         ▼
     ┌───────────────────────┐
     │  🔶 Is Username OR    │
     │     Password empty?    │
     └─┬─────────────────┬───┘
   YES │                 │ NO
       │                 │
       ▼                 ▼
┌──────────────────┐  ┌─────────────────────────┐
│ ⬜ Show Error:   │  │  🔶 Check Username =    │
│ "Enter both      │  │     "admin" AND         │
│  fields"         │  │     Password = "admin123"│
└──────┬───────────┘  └─┬───────────────────┬───┘
       │            YES  │                   │ NO
       │ ◄───────────────┘                   │
       │                                     ▼
       ▼                               ┌─────────────────┐
┌──────────────────┐                  │  🔶 Does        │
│  ⬛ Focus on     │                  │    users.txt    │
│    empty field   │                  │    exist?       │
└──────┬───────────┘                  └─┬───────────┬───┘
       │                           YES  │           │ NO
       │                                │           │
       ▼                                ▼           ▼
┌──────────────────┐                ┌────────────────────────┐
│ ⬛ WAIT FOR      │                │  ⬛ Read users.txt     │
│    INPUT         │                └──────┬─────────────────┘
└──────────────────┘                       │
                                           ▼
                                    ┌────────────────────────┐
                                    │  ⬛ Loop through       │
                                    │    each user record    │
                                    └──────┬─────────────────┘
                                           │
                                           ▼
                                    ┌────────────────────────┐
                                    │  🔶 Match found?       │
                                    └─┬────────────────────┬─┘
                                  YES │                    │ NO
                                      │                    │
                                      ▼                    ▼
                              ┌───────────────────┐  ┌──────────────────┐
                              │ ⬜ Login          │  │ ⬜ Login Failed  │
                              │    Successful!    │  └──┬───────────────┘
                              └───────┬───────────┘     │
                                      │                 ▼
                                      ▼           ┌──────────────────┐
                              ┌───────────────────┐  │ ⬜ Show Error:  │
                              │  ⬛ Open          │  │ "Invalid        │
                              │    Dashboard     │  │  credentials"   │
                              └───────┬───────────┘  └──┬───────────────┘
                                      │                 │
                                      ▼                 ▼
                              ┌───────────────────┐  ┌──────────────────┐
                              │   🔵 END          │  │  ⬛ Clear        │
                              └───────────────────┘  │    Password      │
                                                      └──┬───────────────┘
                                                         │
                                                         ▼
                                                   ┌──────────────────┐
                                                   │ ⬛ WAIT FOR      │
                                                   │    INPUT         │
                                                   └──────────────────┘
```

---

## FLOWCHART 2: Add Book Process

### Flowchart Structure:

```
┌─────────────────┐
│   🔵 START     │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ Display Add Book    │
│     Form                 │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬛ Generate next       │
│    Book ID (BK####)     │
│    auto-generated       │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User fills:         │
│    - Title              │
│    - Author             │
│    - ISBN               │
│    - Quantity           │
│    - Category           │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User clicks         │
│    "Add" button         │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  🔶 Is Title empty?     │
└─┬───────────────────┬───┘
YES│                   │ NO
   │                   │
   ▼                   ▼
┌────────────────┐  ┌─────────────────────────┐
│ ⬜ Show Error: │  │  🔶 Is Author empty?    │
│ "Enter title"  │  └─┬───────────────────┬───┘
└──────┬─────────┘  YES│                   │ NO
       │               │                   │
       │               ▼                   ▼
       │          ┌────────────────┐  ┌─────────────────────────┐
       │          │ ⬜ Show Error: │  │  🔶 Is ISBN numeric?    │
       │          │ "Enter author" │  └─┬───────────────────┬───┘
       │          └──────┬─────────┘   NO│                   │ YES
       │                 │               │                   │
       │                 │               ▼                   ▼
       │                 │          ┌────────────────┐  ┌─────────────────────────┐
       │                 │          │ ⬜ Show Error: │  │  🔶 Is Quantity > 0?    │
       │                 │          │ "Invalid ISBN" │  └─┬───────────────────┬───┘
       │                 │          └──────┬─────────┘   NO│                   │ YES
       │                 │                 │               │                   │
       │                 │                 │               ▼                   ▼
       │                 │                 │          ┌────────────────┐  ┌─────────────────────────┐
       │                 │                 │          │ ⬜ Show Error: │  │  🔶 Is Category         │
       │                 │                 │          │ "Invalid qty"  │  │     selected?           │
       │                 │                 │          └──────┬─────────┘  └─┬───────────────────┬───┘
       │                 │                 │                 │             NO│                   │ YES
       │                 │ ◄───────────────┴─────────────────┘               │                   │
       │                 │                                                   ▼                   ▼
       │                 │                                            ┌────────────────┐  ┌─────────────────────────┐
       │                 │                                            │ ⬜ Show Error: │  │  ⬛ Create book         │
       │                 │                                            │ "Select        │  │    record string        │
       │                 │                                            │  category"     │  │    (pipe-delimited)     │
       │                 │                                            └──────┬─────────┘  └─────────┬───────────────┘
       │                 │                                                   │                      │
       │ ◄───────────────┴───────────────────────────────────────────────────┘                      ▼
       │                                                                                    ┌─────────────────────────┐
       ▼                                                                                    │  ⬛ Append record to    │
┌──────────────────┐                                                                       │    books.txt file       │
│  ⬛ Focus on     │                                                                       └─────────┬───────────────┘
│    error field   │                                                                                │
└──────┬───────────┘                                                                                ▼
       │                                                                                    ┌─────────────────────────┐
       ▼                                                                                    │  ⬜ Show Success:       │
┌──────────────────┐                                                                       │    "Book added!"        │
│ ⬛ WAIT FOR      │                                                                       │    Display Book ID      │
│    INPUT         │                                                                       └─────────┬───────────────┘
└──────────────────┘                                                                                │
                                                                                                    ▼
                                                                                           ┌─────────────────────────┐
                                                                                           │  ⬛ Clear all fields    │
                                                                                           └─────────┬───────────────┘
                                                                                                    │
                                                                                                    ▼
                                                                                           ┌─────────────────────────┐
                                                                                           │  ⬛ Generate new        │
                                                                                           │    Book ID for next     │
                                                                                           └─────────┬───────────────┘
                                                                                                    │
                                                                                                    ▼
                                                                                           ┌─────────────────────────┐
                                                                                           │ ⬛ READY FOR NEXT INPUT│
                                                                                           └─────────────────────────┘
```

---

## FLOWCHART 3: Issue Book Process

### Flowchart Structure:

```
┌─────────────────┐
│   🔵 START     │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ Display Issue       │
│     Books Form          │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User enters         │
│     Book ID             │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User clicks         │
│     "Verify Book"       │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  🔶 Does Book ID        │
│     exist in database?  │
└─┬───────────────────┬───┘
NO│                   │ YES
  │                   │
  ▼                   ▼
┌────────────────┐  ┌─────────────────────────┐
│ ⬜ Show Error: │  │  🔶 Is book quantity    │
│ "Book not      │  │     greater than 0?     │
│  found"        │  └─┬───────────────────┬───┘
└──────┬─────────┘  NO│                   │ YES
       │              │                   │
       │              ▼                   ▼
       │         ┌────────────────┐  ┌─────────────────────────┐
       │         │ ⬜ Show Error: │  │  ⬛ Display book title  │
       │         │ "Book out of   │  │    and author           │
       │         │  stock"        │  │    (verified ✓)         │
       │         └──────┬─────────┘  └─────────┬───────────────┘
       │                │                      │
       │ ◄──────────────┘                      │
       │                                       ▼
       ▼                               ┌─────────────────────────┐
┌──────────────────┐                  │  ⬜ User enters         │
│  ⬛ Clear book   │                  │     Member ID           │
│    title/author  │                  └─────────┬───────────────┘
└──────┬───────────┘                           │
       │                                       ▼
       ▼                               ┌─────────────────────────┐
┌──────────────────┐                  │  ⬜ User clicks         │
│ ⬛ WAIT FOR      │                  │     "Verify Member"     │
│    INPUT         │                  └─────────┬───────────────┘
└──────────────────┘                           │
                                               ▼
                                        ┌─────────────────────────┐
                                        │  🔶 Does Member ID      │
                                        │     exist in database?  │
                                        └─┬───────────────────┬───┘
                                       NO│                   │ YES
                                         │                   │
                                         ▼                   ▼
                                    ┌────────────────┐  ┌─────────────────────────┐
                                    │ ⬜ Show Error: │  │  ⬛ Display member name │
                                    │ "Member not    │  │    (verified ✓)         │
                                    │  found"        │  └─────────┬───────────────┘
                                    └──────┬─────────┘           │
                                           │                     ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬛ Set Issue Date =    │
                                           │              │    Today (auto)         │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬛ Set Due Date =      │
                                           │              │    Today + 14 days      │
                                           │              │    (auto-calculated)    │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬜ User clicks         │
                                           │              │     "Issue Book"        │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  🔶 All validations     │
                                           │              │     pass?               │
                                           │              └─┬───────────────────┬───┘
                                           │             NO│                   │ YES
                                           │               │                   │
                                           │               ▼                   ▼
                                    ┌──────┴──────────┐  ┌─────────────────────────┐
                                    │  ⬛ Clear       │  │  ⬛ Generate unique     │
                                    │    member name  │  │    Issue ID (ISS####)   │
                                    └──────┬──────────┘  └─────────┬───────────────┘
                                           │                       │
                                           │                       ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬛ Create issue record │
                                           │              │    with all details     │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬛ Append to           │
                                           │              │    issued_books.txt     │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬛ Update book         │
                                           │              │    quantity in books.txt│
                                           │              │    (decrease by 1)      │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           │                        ▼
                                           │              ┌─────────────────────────┐
                                           │              │  ⬜ Show Success:       │
                                           │              │    "Book issued!"       │
                                           │              │    Display Issue ID     │
                                           │              │    and Due Date         │
                                           │              └─────────┬───────────────┘
                                           │                        │
                                           ▼                        ▼
                                    ┌──────────────────┐  ┌─────────────────────────┐
                                    │ ⬛ WAIT FOR      │  │  ⬛ Clear all fields    │
                                    │    INPUT         │  │    for next transaction │
                                    └──────────────────┘  └─────────┬───────────────┘
                                                                    │
                                                                    ▼
                                                           ┌─────────────────────────┐
                                                           │ ⬛ READY FOR NEXT ISSUE │
                                                           └─────────────────────────┘
```

---

## FLOWCHART 4: Return Book with Fine Calculation

### Flowchart Structure:

```
┌─────────────────┐
│   🔵 START     │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ Display Return      │
│     Books Form          │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬛ Load all issued     │
│    books with           │
│    Status = "Active"    │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ Display issued      │
│     books in            │
│     DataGridView        │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User selects a      │
│     book to return      │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  ⬜ User clicks         │
│     "Return Book"       │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│  🔶 Is a book selected? │
└─┬───────────────────┬───┘
NO│                   │ YES
  │                   │
  ▼                   ▼
┌────────────────┐  ┌─────────────────────────┐
│ ⬜ Show Error: │  │  ⬛ Get selected book   │
│ "Select a book"│  │    details:             │
└──────┬─────────┘  │    - Issue ID           │
       │            │    - Book ID            │
       │            │    - Due Date           │
       │            └─────────┬───────────────┘
       │                      │
       ▼                      ▼
┌──────────────────┐  ┌─────────────────────────┐
│ ⬛ WAIT FOR      │  │  ⬛ Get return date     │
│    SELECTION     │  │    from DatePicker      │
└──────────────────┘  └─────────┬───────────────┘
                                │
                                ▼
                         ┌─────────────────────────┐
                         │  🔶 Is Return Date >    │
                         │     Due Date?           │
                         │     (LATE RETURN?)      │
                         └─┬───────────────────┬───┘
                        NO│                   │ YES
                          │                   │
                          ▼                   ▼
                    ┌───────────────┐  ┌─────────────────────────┐
                    │  ⬛ fine = 0  │  │  ⬛ Calculate days late  │
                    │    (On time)  │  │    = Return Date -      │
                    └───────┬───────┘  │      Due Date           │
                            │          └─────────┬───────────────┘
                            │                    │
                            │                    ▼
                            │          ┌─────────────────────────┐
                            │          │  ⬛ Calculate fine:     │
                            │          │    fine = days late ×   │
                            │          │           $0.50         │
                            │          └─────────┬───────────────┘
                            │                    │
                            ▼◄───────────────────┘
                     ┌─────────────────────────┐
                     │  ⬜ Display fine info:  │
                     │    IF fine > 0:         │
                     │      "Late: $X.XX"      │
                     │      (red text)         │
                     │    ELSE:                │
                     │      "On Time: $0.00"   │
                     │      (green text)       │
                     └─────────┬───────────────┘
                               │
                               ▼
                     ┌─────────────────────────┐
                     │  ⬜ Show confirmation:  │
                     │    "Return book?"       │
                     │    Display fine amount  │
                     └─┬───────────────────┬───┘
                    NO│                   │ YES
                      │                   │
                      ▼                   ▼
            ┌────────────────┐  ┌─────────────────────────┐
            │ ⬛ Cancel      │  │  ⬛ Update issue record │
            │    return      │  │    Status = "Returned"  │
            │                │  │    Add Return Date      │
            └──────┬─────────┘  │    Add Fine Amount      │
                   │            └─────────┬───────────────┘
                   │                      │
                   ▼                      ▼
            ┌──────────────────┐  ┌─────────────────────────┐
            │ ⬛ WAIT FOR      │  │  ⬛ Save updated record │
            │    SELECTION     │  │    to issued_books.txt  │
            └──────────────────┘  └─────────┬───────────────┘
                                            │
                                            ▼
                                  ┌─────────────────────────┐
                                  │  ⬛ Update book quantity│
                                  │    in books.txt         │
                                  │    (increase by 1)      │
                                  └─────────┬───────────────┘
                                            │
                                            ▼
                                  ┌─────────────────────────┐
                                  │  ⬜ Show Success:       │
                                  │    "Book returned!"     │
                                  │    IF fine > 0:         │
                                  │      Display fine       │
                                  └─────────┬───────────────┘
                                            │
                                            ▼
                                  ┌─────────────────────────┐
                                  │  ⬛ Refresh issued      │
                                  │    books list           │
                                  │    (exclude returned)   │
                                  └─────────┬───────────────┘
                                            │
                                            ▼
                                  ┌─────────────────────────┐
                                  │ ⬛ READY FOR NEXT RETURN│
                                  └─────────────────────────┘
```

---

## HOW TO USE THESE FLOWCHARTS

### Option 1: Create in Microsoft Word

1. **Insert → SmartArt → Process**
2. Choose "Basic Block List" or "Vertical Block List"
3. Add shapes as needed
4. Format shapes:
   - Ovals for Start/End (blue fill)
   - Rectangles for processes (gray fill)
   - Diamonds for decisions (yellow fill)
   - Parallelograms for I/O (green fill)
5. Connect with arrows

### Option 2: Use Word Shapes

1. **Insert → Shapes**
2. Add individual shapes:
   - Flowchart: Terminator (Start/End)
   - Flowchart: Process (rectangles)
   - Flowchart: Decision (diamond)
   - Flowchart: Data (parallelogram)
3. Add text to each shape
4. Use connector arrows

### Option 3: Use Draw.io (Recommended)

1. Go to app.diagrams.net
2. Choose "Flowchart" template
3. Drag and drop shapes from left panel
4. Connect with arrows
5. Export as PNG or PDF
6. Insert into Word document

### Option 4: Use Lucidchart

1. Create free account at lucidchart.com
2. New Document → Flowchart
3. Use shapes from library
4. Connect and label
5. Export and insert into Word

---

## Flowchart Color Scheme (Recommended)

- **Start/End (Oval):** Light Blue (#3498DB)
- **Process (Rectangle):** Light Gray (#ECF0F1)
- **Decision (Diamond):** Light Yellow (#F39C12)
- **Input/Output (Parallelogram):** Light Green (#2ECC71)
- **Arrows:** Black
- **Text:** Black, Arial 10pt

---

## Tips for Professional Flowcharts

1. **Alignment:** Keep all shapes properly aligned
2. **Spacing:** Maintain consistent spacing between shapes
3. **Arrow Direction:** Generally top-to-bottom, left-to-right
4. **Labels:** YES/NO labels on decision branches
5. **Size:** All shapes of same type should be same size
6. **Borders:** Use same border thickness throughout

---

*Use this guide to create professional flowcharts for your report!*
