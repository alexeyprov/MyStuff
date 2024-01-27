package main

import (
	crand "crypto/rand"
	"fmt"
	"math/rand"
)

func longestSubarray(nums []int) int {
	n := len(nums)
	if n == 0 {
		return 0
	}

	best := 0
	lastChunk, thisChunk := 0, 0
	isLastZero := false
	hasZeroes := false

	for _, v := range nums {
		if v == 0 {

			hasZeroes = true
			if isLastZero {
				lastChunk = 0
			} else {
				sum := lastChunk + thisChunk
				if sum > best {
					best = sum
				}

				lastChunk = thisChunk
			}

			thisChunk = 0
			isLastZero = true

		} else {

			thisChunk++
			isLastZero = false

		}
	}

	if !hasZeroes {
		return n - 1
	}

	sum := lastChunk + thisChunk
	if sum > best {
		return sum
	}

	return best
}

func runTest(nums []int, expected ...int) {
	actual := longestSubarray(nums)

	fmt.Printf("%v => %d\n", nums, actual)
	if expected == nil || len(expected) == 0 {
		return
	}

	if expected[0] != actual {
		err := fmt.Sprintf("Expected %d, got %d", expected[0], actual)
		panic(err)
	}
}

func main() {
	// original #1
	runTest([]int{1, 1, 0, 1}, 3)

	// original #2
	runTest([]int{0, 1, 1, 1, 0, 1, 1, 0, 1}, 5)

	// original #3
	runTest([]int{1, 1, 1}, 2)

	// random
	n := rand.Intn(4) + 1
	bytes := make([]byte, n)
	nums := make([]int, n*8)
	_, _ = crand.Read(bytes)
	idx := 0
	for _, b := range bytes {
		for i := 1; i < 256; i <<= 1 {
			if (int(b) & i) == i {
				nums[idx] = 1
			} else {
				nums[idx] = 0
			}

			idx++
		}
	}

	runTest(nums)
}
