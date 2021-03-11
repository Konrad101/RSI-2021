import org.apache.xmlrpc.XmlRpcClient;

import java.util.Vector;

public class ServerData {
    private final String serverAddress;
    private final String serverPort;

    public ServerData(String serverAddress, String serverPort) {
        this.serverAddress = serverAddress;
        this.serverPort = serverPort;
    }

    public Object runService(String serviceName, Vector<Object> parameters) {
        Object result = null;
        try {
            XmlRpcClient srv = new XmlRpcClient("http://" + serverAddress + ":" + serverPort);
            result = srv.execute("MojSerwer." + serviceName, parameters);
        } catch (Exception exception) {
            System.err.println("Klient XML-RPC: " + exception);
        }

        return result;
    }

}
