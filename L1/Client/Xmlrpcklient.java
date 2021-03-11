import org.apache.xmlrpc.XmlRpcClient;

import java.text.SimpleDateFormat;
import java.util.*;

public class Xmlrpcklient {
    private static final int SERVER_PORT = 5001;

    private static void printInfo() {
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Date date = new Date(System.currentTimeMillis());
        System.out.println(formatter.format(date));
        System.out.println("User: " + System.getProperty("user.name") + "\n");
    }

    private static ServerData getServerDataFromUser() {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Enter server address: ");
        String address = scanner.next();
        System.out.println("Enter server port: ");
        String port = scanner.next();

        return new ServerData(address, port);
    }

    private static Service getServiceDataFromUser(ServerData serverData) {
        try {
            Vector<Object> parameters = new Vector<>();
            Object res = serverData.runService("show", parameters);
            Vector<String> methodsList = (Vector<String>) res;

            for (String data : methodsList) {
                System.out.println(data);
            }

            List<String> methodNames = filterMethodNames(methodsList);
            String methodNameFromUser;
            Scanner scanner = new Scanner(System.in);

            do {
                System.out.println("Please enter method name:");
                methodNameFromUser = scanner.next();
            } while (!methodNames.contains(methodNameFromUser));

            parameters = createParametersVector(methodsList, methodNameFromUser, scanner);

            return new Service(methodNameFromUser, parameters);
        } catch (Exception exception) {
            System.err.println("Klient XML-RPC: " + exception);
        }

        return null;
    }

    private static Vector<Object> createParametersVector(Vector<String> methodsList,
                                                         String methodNameFromUser,
                                                         Scanner scanner) {
        Vector<Object> parameters = new Vector<>();
        List<String> parametersTypes = filterMethodParametersTypes(methodsList, methodNameFromUser);
        for (String parameter : parametersTypes) {
            System.out.print(parameter + " = ");
            String value = scanner.next();
            String parameterType = parameter.split(" ")[0];
            if (parameterType.equals("int")) {
                parameters.addElement(Integer.parseInt(value));
            } else if (parameterType.equals("boolean")) {
                parameters.addElement(Boolean.parseBoolean(value));
            } else {
                parameters.addElement(value);
            }
        }

        return parameters;
    }

    private static List<String> filterMethodNames(Vector<String> methodsList) {
        List<String> methodNames = new ArrayList<>();
        for (String method : methodsList) {
            String[] methodData = method.split("\n");
            methodNames.add(methodData[0].replace("Method: ", ""));
        }
        return methodNames;
    }

    private static List<String> filterMethodParametersTypes(Vector<String> methodsList, String methodName) {
        List<String> parametersTypes = new Vector<>();
        for (String method : methodsList) {
            String[] methodData = method.split("\n");
            String currentMethod = methodData[0].replace("Method: ", "");
            if (currentMethod.equals(methodName)) {
                String[] parameters = methodData[1].replace("Arguments: ", "").split(", ");

                Collections.addAll(parametersTypes, parameters);
            }
        }

        return parametersTypes;
    }

    public static void main(String[] args) {
        printInfo();
        ServerData serverData = getServerDataFromUser();
        Service service = getServiceDataFromUser(serverData);

        System.out.println(serverData.runService(service.getMethodName(), service.getParameters()));
    }

    public static void testServerMethods() {
        try {
            XmlRpcClient srv = new XmlRpcClient("http://localhost:" + SERVER_PORT);
            Vector<Object> parseLocalDateParams = new Vector<>();
            parseLocalDateParams.addElement("2020-01-01");
            parseLocalDateParams.addElement("yyyy-MM-dd");
            parseLocalDateParams.addElement(true);
            Object result = srv.execute("MojSerwer.parseDateToDateFormat", parseLocalDateParams);
            System.out.println((String) result);

            Vector<Object> getResultParams = new Vector<>();
            getResultParams.addElement(1);
            getResultParams.addElement("el Fras");
            getResultParams.addElement(true);
            result = srv.execute("MojSerwer.getResult", getResultParams);
            System.out.println("EL RESULTATO:\n" + result);

            Vector<Integer> asyncParams = new Vector<>();
            asyncParams.addElement(2000);

            AC cb = new AC();
            srv.executeAsync("MojSerwer.checkTime", asyncParams, cb);

            Vector<Object> par = new Vector<>();
            Object res = srv.execute("MojSerwer.show", par);
            Vector<String> methodsList = (Vector<String>) res;
            for (String method : methodsList) {
                System.out.println(method);
            }
        } catch (Exception exception) {
            System.err.println("Klient XML-RPC: " + exception);
        }
    }
}
