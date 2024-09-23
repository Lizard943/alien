#include <iostream>
using namespace std;
 
// Lớp cha
class Mayvitinh
{
public:
    Mayvitinh()
    {
        cout << "This is a computer's brand" << endl;
    }
};

// Lớp con kế thừa từ lớp cha
class Maylaptop : public Mayvitinh
{
public:
    Maylaptop()
    {
        cout << "This is a laptop's brand" << endl;
    }

    void test(){
        cout << "This is a laptop's brand" << endl;
    }
};
 
// Lớp con kế thừa từ lớp cha thứ 2
class mayAcer : public Maylaptop
{
public:
     mayAcer(){
         cout << "This brand is Acer" << endl;
     }
};
 
// main function
int main()
{
    Mayvitinh a;
    a.test();
    return 0;
}