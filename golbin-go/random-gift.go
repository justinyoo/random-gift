package main

import (
	"bufio"
	"encoding/csv"
	"fmt"
	"io"
	"math/rand"
	"os"
	"time"
	"strconv"
)

type CSVData [][]string
type Product []string

func Shuffle(s CSVData) {
	rand.Seed(time.Now().UnixNano())

	for i := range s {
		j := rand.Intn(i + 1)
		s[i], s[j] = s[j], s[i]
	}
}

func ReadCSV(filename string) CSVData {
	lines := CSVData{}

	f, err := os.Open(filename)
	if err != nil {
		panic(err)
	}
	defer f.Close()

	r := csv.NewReader(bufio.NewReader(f))
	for {
		row, err := r.Read()

		if err == io.EOF {
			break
		}

		lines = append(lines, row)
	}

	return lines
}

func (pd Product) RetrieveProduct(count, winner int, email CSVData) Product {
	return Product{
		pd[0], // 상품
		strconv.Itoa(count+1), // 상품 순서
		email[winner][0], // 당첨자 이메일
	}
}

func RepeatPrize() func (pd Product, email CSVData) CSVData {
	total := 0

	return func (pd Product, email CSVData) CSVData {
		winner := CSVData{}
		subtotal, _ := strconv.Atoi(pd[1])

		for i := 0; i < subtotal; i++ {
			winner = append(winner, pd.RetrieveProduct(i, total, email))
			total++
		}

		return winner
	}
}

func DrawPrize(product CSVData, email CSVData) CSVData {
	winner := CSVData{}

	Shuffle(email)

	repeatPrize := RepeatPrize()

	for _, p := range product {
		winner = append(winner, repeatPrize(p, email)...)
	}

	return winner
}

func main() {
	if len(os.Args) < 3 {
		fmt.Println("Usage: go run random-gift2.go product.csv email.csv")
		return
	}

	product := ReadCSV(os.Args[1])
	email := ReadCSV(os.Args[2])

	winner := DrawPrize(product, email)

	for _, w := range winner {
		fmt.Printf("%s - %s - %s\n", w[0], w[1], w[2])
	}
}
