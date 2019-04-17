package main

import (
	"context"
	"log"
	"time"

	pb "./jjtask"

	"google.golang.org/grpc"
)

const (
	address = "localhost:80"
	//address = "40.74.17.178:80"
)

func main() {

	// Set up a connection to the server.
	conn, err := grpc.Dial(address, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	defer conn.Close()

	// call client
	c := pb.NewJJTaskManagerClient(conn)
	log.Printf("JJTaskManager client created...")

	ctx, cancel := context.WithTimeout(context.Background(), time.Second)
	defer cancel()

	// call client to add task
	r, err := c.AddTask(ctx, &pb.JJTask{Name: "Muj task"})
	if err != nil {
		log.Fatalf("could not add task: %v", err)
	}

	log.Printf("Result: %s", r.Name)
}
