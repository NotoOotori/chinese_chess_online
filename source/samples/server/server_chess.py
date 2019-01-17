''' A Timestamp TCP Server.'''
import socket as sock
from time import ctime

import _thread as thread

HOST = 'localhost'
PORT = 21566
BUFSIZ = 1024
ADDR = (HOST, PORT)
clients = []

def client_thread(tcp_cli_sock, addr):
    ''' A client thread.'''
    while True:
        try:
            data = tcp_cli_sock.recv(BUFSIZ).decode('utf-8')
        except ConnectionResetError:
            data = None
        if not data:
            print(addr, 'disconnected')
            clients.remove(tcp_cli_sock)
            tcp_cli_sock.close()
            break
        print(addr, ':', data)
        broadcast(data.encode('utf-8'), tcp_cli_sock)
        # tcp_cli_sock.send(
        #     '[{}] {}'.format(ctime(), data).encode('utf-8'))

def main():
    ''' The main function.'''
    tcp_ser_sock = sock.socket(sock.AF_INET, sock.SOCK_STREAM)
    tcp_ser_sock.bind(ADDR)
    tcp_ser_sock.listen(5)

    while True:
        tcp_cli_sock, addr = tcp_ser_sock.accept()

        clients.append(tcp_cli_sock)
        thread.start_new_thread(client_thread, (tcp_cli_sock, addr))
    tcp_ser_sock.close()

def broadcast(message, source_client):
    for client in clients: 
        if client != source_client:
            try: 
                client.send(message) 
            except: 
                client.close() 
  
                # if the link is broken, we remove the client 
                clients.remove(client)

if __name__ == '__main__':
    main()
