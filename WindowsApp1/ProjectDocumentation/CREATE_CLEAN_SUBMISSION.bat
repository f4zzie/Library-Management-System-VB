@echo off
echo Creating clean submission folder...

REM Create submission folder
mkdir LibraryManagementSystem_Submission 2>nul
mkdir LibraryManagementSystem_Submission\SourceCode 2>nul
mkdir "LibraryManagementSystem_Submission\SourceCode\My Project" 2>nul

REM Copy VB code files
copy Form1.vb "LibraryManagementSystem_Submission\SourceCode\"
copy Form1.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy Form1.resx "LibraryManagementSystem_Submission\SourceCode\"
copy Dashboard.vb "LibraryManagementSystem_Submission\SourceCode\"
copy Dashboard.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy AddBooks.vb "LibraryManagementSystem_Submission\SourceCode\"
copy AddBooks.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ViewBooks.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ViewBooks.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy AddMembers.vb "LibraryManagementSystem_Submission\SourceCode\"
copy AddMembers.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ViewMembers.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ViewMembers.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy IssueBooks.vb "LibraryManagementSystem_Submission\SourceCode\"
copy IssueBooks.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ReturnBooks.vb "LibraryManagementSystem_Submission\SourceCode\"
copy ReturnBooks.Designer.vb "LibraryManagementSystem_Submission\SourceCode\"

REM Copy project files
copy WindowsApp1.vbproj "LibraryManagementSystem_Submission\SourceCode\"
copy WindowsApp1.slnx "LibraryManagementSystem_Submission\SourceCode\"
copy App.config "LibraryManagementSystem_Submission\SourceCode\"

REM Copy sample data
copy sample_books.txt "LibraryManagementSystem_Submission\SourceCode\"
copy sample_members.txt "LibraryManagementSystem_Submission\SourceCode\"
copy sample_issued_books.txt "LibraryManagementSystem_Submission\SourceCode\"

REM Copy My Project folder
xcopy "My Project" "LibraryManagementSystem_Submission\SourceCode\My Project" /E /I /Y

REM Copy README (cleaned version without helper docs)
copy README.md "LibraryManagementSystem_Submission\SourceCode\"

echo.
echo =============================================
echo CLEAN SUBMISSION FOLDER CREATED!
echo =============================================
echo.
echo Location: LibraryManagementSystem_Submission\
echo.
echo NO .vs folder included!
echo NO bin/obj folders included!
echo NO helper documentation included!
echo.
echo Next steps:
echo 1. Add your Word document to LibraryManagementSystem_Submission\
echo 2. Add your PDF to LibraryManagementSystem_Submission\
echo 3. Zip the entire LibraryManagementSystem_Submission folder
echo 4. Submit the ZIP file!
echo.
pause
