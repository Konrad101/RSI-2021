import org.apache.xmlrpc.AsyncCallback;

import java.net.URL;

public class AC implements AsyncCallback {
    @Override
    public void handleResult(Object rezultat, URL url, String metoda) {
        System.out.println("Przechwycono rezultat: " + rezultat + " | metoda: " + metoda);
        System.out.println("URL: " + url);
    }

    @Override
    public void handleError(Exception e, URL url, String metoda) {
        System.out.println("Przechwycono błąd: " + e.getMessage() + " | metoda: " + metoda);
        System.out.println("URL: " + url);
    }
}
