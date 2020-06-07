to run hosting:
netsh>http add urlacl url=http://+:9876/ user=everyone

Note: we use port 9876


(Optional) Add an HTTP URL Namespace Reservation
This application listens to http://localhost:9876/. By default, listening at a particular HTTP address requires administrator privileges. When you run the tutorial, therefore, you may get this error: "HTTP could not register URL http://+:8080/" There are two ways to avoid this error:

Run Visual Studio with elevated administrator permissions, or
Use Netsh.exe to give your account permissions to reserve the URL.
To use Netsh.exe, open a command prompt with administrator privileges and enter the following command:following command:

Console

Copy
netsh http add urlacl url=http://+:9876/ user=machine\username
where machine\username is your user account.

When you are finished self-hosting, be sure to delete the reservation:

Console 

Copy
netsh http delete urlacl url=http://+:9876/