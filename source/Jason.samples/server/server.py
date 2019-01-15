''' A Timestamp TCP Server.'''
import socket as sock
from time import ctime

import _thread as thread

HOST = '45.32.82.133'
PORT = 21567
BUFSIZ = 1024
ADDR = (HOST, PORT)

def client_thread(tcp_cli_sock, addr):
    ''' A client thread.'''
    while True:
        data = tcp_cli_sock.recv(BUFSIZ).decode('utf-8')
        if not data:
            print(addr, 'disconnected')
            tcp_cli_sock.close()
            break
        print(addr, ':', data)
        tcp_cli_sock.send(
            '[{}] {}'.format(ctime(), data).encode('utf-8'))

def main():
    ''' The main function.'''
    tcp_ser_sock = sock.socket(sock.AF_INET, sock.SOCK_STREAM)
    tcp_ser_sock.bind(ADDR)
    tcp_ser_sock.listen(5)

    while True:
        tcp_cli_sock, addr = tcp_ser_sock.accept()

        thread.start_new_thread(client_thread, (tcp_cli_sock, addr))
    tcp_ser_sock.close()

if __name__ == '__main__':
    main()
