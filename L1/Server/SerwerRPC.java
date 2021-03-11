import org.apache.xmlrpc.WebServer;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class SerwerRPC {

    public static void main(String[] args) {
        try {
            System.out.println("Startuje serwer XML-RPC...");
            int port = 5001;
            WebServer server = new WebServer(port);

            server.addHandler("MojSerwer", new SerwerRPC());
            server.start();

            System.out.println("Serwer wystartowal pomyslnie.");
            System.out.println("Nasluchuje na porcie: " + port);
            System.out.println("Aby zatrzymac serwer nacisnij crl+c");
        } catch (Exception exception) {
            System.err.println("Serwer XML-RPC: " + exception);
        }
    }

    public String parseDateToDateFormat(String dateStr, String printFormat, boolean showLeapYearInfo) {
        if (dateStr.equals("")) {
            return null;
        } else if (printFormat.equals("")) {
            printFormat = "dd.MM.yyyy";
        }
        LocalDate date = LocalDate.parse(dateStr);
        String leapYearInfo = showLeapYearInfo ?
                date.isLeapYear() ? "The year is leap" : "The year is not leap"
                : "";
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern(printFormat);
        return formatter.format(date) + " " + leapYearInfo;
    }

    public String getResult(int number, String name, boolean isMale) {
        String gender = isMale ? "Male" : "Female";
        return "Number: " + number + "\n" +
                "Name: " + name + "\n" +
                "Gender: " + gender;
    }

    public Boolean checkTime(int timeToSleep) {
        System.out.println("I'm starting asynchronous method");
        try {
            Thread.sleep(timeToSleep);
        } catch (InterruptedException e) {
            e.printStackTrace();
            Thread.currentThread().interrupt();
        }
        System.out.println("End of the async");
        long time = System.currentTimeMillis();
        return time % 2 == 0;
    }

    public String[] show() {
        String[] list = new String[3];
        list[0] = "Method: parseDateToDateFormat\n" +
                "Arguments: String dateStr, String printFormat, boolean showLeapYearInfo\n" +
                "Description: Konrad wypelni.\n";
        list[1] = "Method: getResult\n" +
                "Arguments: int number, String name, boolean isMale\n" +
                "Description: Konrad wypelni.\n";
        list[2] = "Method: checkTime\n" +
                "Arguments: int timeToSleep\n" +
                "Description: Simple method to check if current system time in millis is odd.\n";
        return list;
    }
}
