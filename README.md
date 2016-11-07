# select_topic
Select_Topic_Class_2016

install mono on ubuntu for C# Compilers
1. 
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | tee /etc/apt/sources.list.d/mono-xamarin.list
apt-get update
apt-get install Mono-Complete
REF : http://www.mono-project.com/…/getting-started/install/linux/
TEST:
using System;
public class HelloWorld
{
static public void Main ()
{
Console.WriteLine ("Hello Mono World");
}
}
Compile :
mcs name-file .cs
mono name-file .exe
2.Install Web Server XSP
sudo apt-get install mono-xsp4
some file misssing
sudo apt-get install asp.net-examples
TEST(Open Math web Service) :
mkdir Select_Topic
cd Select_Topic
nano NumberService.asmx
CODE:
<%@ WebService Language="C#" Class="MathService.MathService" %>
using System;
using System.Web.Services;
namespace MathService
{
[WebService (Namespace = "http://tempuri.org/NumberService")]
public class MathService : WebService
{
[WebMethod]
public int AddNumbers (int number1, int number2)
{
return number1 + number2;
}
[WebMethod]
public int SubtractNumbers (int number1, int number2)
{
return number1 - number2;
}
}
}
Run:
xsp4 --port 8000 --root /home/___user/ ... /Select_Topic/
open web:
https://localhost:9000/Numberservice.asmx
3. Download Code form s3 and Run Service:
wget https://s3-ap-southeast-1.amazonaws.com/…/NewWebService.asmx
Copy to /Select_Topic/
Run :
xsp4 --port 8000 --root /home/....user/ .... /Select_Topic/
On Cloud :
xsp4 --port 8000 --root /home/....user/ ..../Select_Topic/ --address public_DNS_form_EC2(use for SSH)
Test:
public_DNS_form_EC2(use for SSH):8000/NewWebService.asmx

