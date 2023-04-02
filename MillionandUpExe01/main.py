import subprocess
import multiprocessing
import webbrowser
import http.server
import socketserver


class Handler(http.server.SimpleHTTPRequestHandler):
    def send_response(self, *args, **kwargs):
        http.server.SimpleHTTPRequestHandler.send_response(self, *args, **kwargs)
        self.send_header('Access-Control-Allow-Origin', '*')


def run_back():
    command = 'cd back & MillionandUpBackend01.exe'
    process = subprocess.Popen(command, shell=True, stdout=subprocess.PIPE)
    process.wait()

def run_front():
    webbrowser.open_new('http://localhost:8080/front/#/')
    PORT = 8080
    with socketserver.TCPServer(("", PORT), Handler) as httpd:
        httpd.serve_forever()

if __name__=='__main__':
    multiprocessing.freeze_support()

    p1 = multiprocessing.Process(target=run_back)
    p2 = multiprocessing.Process(target=run_front)

    p1.start()
    p2.start()
    p1.join()
    p2.join()



# dotnet build "MillionandUpBackend01.csproj" -c Release -o /app/build -r win-x64 --self-contained true
# quasar build
# python -m venv MillionandUpPortable01
